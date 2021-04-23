using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilemapScript : MonoBehaviour
{
    [SerializeField]  private GameObject[] towerToPlace = new GameObject[3];
    [SerializeField]  private GameObject gridSelection;
    [SerializeField]  private Button mergeBtn;
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
                GameObject placedTower = (GameObject) Instantiate(towerToPlace[Random.Range(0, 3)], cellWorldPos, Quaternion.identity);
                Debug.Log($"placed tower: {placedTower.GetComponent<TowerBehaviour>().towerName}");
                this.towerPlacements[matrixPosX, matrixPosY] = placedTower;
            }
        } else if (clickedTile.name == "53") {
            ResetSelections();
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

            if (selectedPos1 != notSetPos && selectedPos2 != notSetPos) {
                mergeBtn.gameObject.SetActive(!mergeBtn.IsActive());
            }
        }
    }

    int GetMatrixPosFromCellGridPosX(int x) {
        return x + tilemap.cellBounds.size.x / 2;
    }

    int GetMatrixPosFromCellGridPosY(int y) {
        return y + tilemap.cellBounds.size.y / 2;
    }

    void ResetSelections() {
        selectedPos1 = notSetPos;
        selectedPos2 = notSetPos;
        gridSelection1.transform.position = notSetPos;
        gridSelection2.transform.position = notSetPos;
        mergeBtn.gameObject.SetActive(false);
    }

    public void MergeTowers() {
        Debug.Log("Merging towers!");

        Vector3Int cellGridPost1 = grid.WorldToCell(selectedPos1);
        int matrixPosXt1 = GetMatrixPosFromCellGridPosX((int) cellGridPost1.x);
        int matrixPosYt1 = GetMatrixPosFromCellGridPosY((int) cellGridPost1.y);
        GameObject t1 = this.towerPlacements[matrixPosXt1, matrixPosYt1];

        Vector3Int cellGridPost2 = grid.WorldToCell(selectedPos2);
        int matrixPosXt2 = GetMatrixPosFromCellGridPosX((int) cellGridPost2.x);
        int matrixPosYt2 = GetMatrixPosFromCellGridPosY((int) cellGridPost2.y);
        GameObject t2 = this.towerPlacements[matrixPosXt2, matrixPosYt2];

        // Debug.Log($"T1 name: {t1.GetComponent<TowerBehaviour>().towerName}");

        if (t1.GetComponent<TowerBehaviour>().towerName == t2.GetComponent<TowerBehaviour>().towerName && t1.GetComponent<TowerBehaviour>().level == t2.GetComponent<TowerBehaviour>().level) {
            Destroy(t2);
            this.towerPlacements[matrixPosXt2, matrixPosYt2] = null;
            t1.GetComponent<TowerBehaviour>().Upgrade();
            ResetSelections();
        }
    }
}
