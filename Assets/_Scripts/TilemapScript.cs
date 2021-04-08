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
        
        Debug.Log($"{tilemap.cellBounds}");
    }

    void OnMouseDown()
    {
        if (gm.GetMoney() >= 100) {
            Debug.Log("Click on tilemap!");

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellGridPos = grid.WorldToCell(mousePos);
            Vector3 cellWorldPos = grid.CellToWorld(cellGridPos);
            TileBase clickedTile = tilemap.GetTile(cellGridPos);

            cellWorldPos += grid.cellSize / 2;
            // Debug.Log(cellWorldPos.y);
            cellWorldPos.y += grid.cellSize.y / 2;
            Debug.Log(cellWorldPos);

            if (clickedTile.name == "52")
            {
                gm.PurchaseTower();
                Instantiate(towerToPlace[Random.Range(0, 3)], cellWorldPos, Quaternion.identity);
            }
        }
    }
}
