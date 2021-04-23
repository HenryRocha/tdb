using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapScript : MonoBehaviour
{
    [SerializeField]  private GameObject[] towerToPlace = new GameObject[3];
    [SerializeField]  private GameObject gridSelection;
    private GameObject gridSelection1;
    private GameObject gridSelection2;
    private Tilemap tilemap;
    private Grid grid;
    private GameObject[,] towerPlacements;
    private GameManager gm;
    private static Vector3 notSetPos = new Vector3(-200, -200, -200);
    private Vector3 selectedPos1 = notSetPos;
    private Vector3 selectedPos2 = notSetPos;

    // Start is called  before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grid = transform.parent.GetComponentInParent<Grid>();
        gm = GameManager.GetInstance();
        gridSelection1 = Instantiate(gridSelection, new Vector3(-15, -15, 0), Quaternion.identity);
        gridSelection2 = Instantiate(gridSelection, new Vector3(-15, -15, 0), Quaternion.identity);

        this.towerPlacements = new GameObject[tilemap.cellBounds.size.x, tilemap.cellBounds.size.y];
    }

    void OnMouseDown()
    {
        Debug.Log("Click on tilemap!");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellGridPos = grid.WorldToCell(mousePos);
        Vector3 cellWorldPos = grid.CellToWorld(cellGridPos);
        TileBase clickedTile = tilemap.GetTile(cellGridPos);

        int matrixPosX = GetMatrixPosFromCellGridPosX(cellGridPos.x);
        int matrixPosY = GetMatrixPosFromCellGridPosY(cellGridPos.y);

    
        if (clickedTile.name == "52" && !this.towerPlacements[matrixPosX, matrixPosY])
        {
            if (gm.GetMoney() >= gm.GetTowerCost()) {
                cellWorldPos += grid.cellSize / 2;
                cellWorldPos.y += grid.cellSize.y / 2;

                gm.PurchaseTower();
                GameObject placedTower = Instantiate(towerToPlace[Random.Range(0, 3)], cellWorldPos, Quaternion.identity);
                this.towerPlacements[matrixPosX, matrixPosY] = placedTower;
            }
        } else if (clickedTile.name == "53") {
            selectedPos1 = notSetPos;
            selectedPos2 = notSetPos;
            gridSelection1.transform.position = notSetPos;
            gridSelection2.transform.position = notSetPos;
        } else if (clickedTile.name == "52" && this.towerPlacements[matrixPosX, matrixPosY]) {
            Debug.Log("Selected tower!");

            if (selectedPos1 == notSetPos) {
                selectedPos1 = cellWorldPos;
                selectedPos1 += grid.cellSize / 2;

                if (selectedPos1 != selectedPos2) {
                    gridSelection1.transform.position = selectedPos1;
                }
            } else if (selectedPos2 == notSetPos) {
                selectedPos2 = cellWorldPos;
                selectedPos2 += grid.cellSize / 2;

                if (selectedPos2 != selectedPos1) {
                    gridSelection2.transform.position = selectedPos2;
                } else {
                    selectedPos2 = notSetPos;
                }
            }

            // if (selectedPos2 == notSetPos && selectedPos1 != notSetPos) {
            //     selectedPos2 = cellWorldPos;
            //     selectedPos2 += grid.cellSize / 2;

            //     gridSelection2.transform.position = selectedPos2;
            // } else if (selectedPos1 == notSetPos) {
            //     selectedPos1 = cellWorldPos;
            //     selectedPos1 += grid.cellSize / 2;

            //     gridSelection1.transform.position = selectedPos1;
            // }
        }
    }

    int GetMatrixPosFromCellGridPosX(int x) {
        return x + tilemap.cellBounds.size.x / 2;
    }

    int GetMatrixPosFromCellGridPosY(int y) {
        return y + tilemap.cellBounds.size.y / 2;
    }
}
