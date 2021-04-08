using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapScript : MonoBehaviour
{
    [SerializeField]  private GameObject[] towerToPlace = new GameObject[3];
    private Tilemap tilemap;
    private Grid grid;
    private bool[,] towerPlacements;
    private GameManager gm;

    // Start is called  before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grid = transform.parent.GetComponentInParent<Grid>();
        gm = GameManager.GetInstance();
        
        this.towerPlacements = new bool[tilemap.cellBounds.size.x, tilemap.cellBounds.size.y];
    }

    void OnMouseDown()
    {
        if (gm.GetMoney() >= 100) {
            Debug.Log("Click on tilemap!");

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellGridPos = grid.WorldToCell(mousePos);
            Vector3 cellWorldPos = grid.CellToWorld(cellGridPos);
            TileBase clickedTile = tilemap.GetTile(cellGridPos);

            int matrixPosX = GetMatrixPosFromCellGridPosX(cellGridPos.x);
            int matrixPosY = GetMatrixPosFromCellGridPosY(cellGridPos.y);

            cellWorldPos += grid.cellSize / 2;
            cellWorldPos.y += grid.cellSize.y / 2;

            if (clickedTile.name == "52" && !this.towerPlacements[matrixPosX, matrixPosY])
            {
                gm.PurchaseTower();
                Instantiate(towerToPlace[Random.Range(0, 3)], cellWorldPos, Quaternion.identity);
                this.towerPlacements[matrixPosX, matrixPosY] = true;
            }
        }
    }

    int GetMatrixPosFromCellGridPosX(int x) {
        return x + tilemap.cellBounds.size.x / 2;
    }

    int GetMatrixPosFromCellGridPosY(int y) {
        return y + tilemap.cellBounds.size.y / 2;
    }
}
