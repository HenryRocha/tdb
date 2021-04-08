using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapScript : MonoBehaviour
{
    [SerializeField]  private GameObject towerToPlace;
    private Tilemap tilemap;
    private Grid grid;
    private bool[,] towerPlacements;

    // Start is called  before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grid = transform.parent.GetComponentInParent<Grid>();


        Debug.Log($"{tilemap.cellBounds}");
    }

    void OnMouseDown()
    {
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
            Instantiate(towerToPlace, cellWorldPos, Quaternion.identity);
        }
    }
}
