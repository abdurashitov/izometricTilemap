using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapContorller : MonoBehaviour
{

    public TileBase TileToSet;
    public Tilemap map;
    private Camera mainCamera;
    private TileBase currentTile;
    public TileBase[] rotationRoads;
    public TileBase[] crossRoads;
    public TileBase[] Roads;
    public TileBase[] decoration;
    // Start is called before the first frame update
    void Start()
    {
        //map = GetComponent<Tilemap>();
        mainCamera = Camera.main;
        Debug.Log(rotationRoads.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clicWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickCellPosition = map.WorldToCell(clicWorldPosition);
            clickCellPosition = new Vector3Int(clickCellPosition.x, clickCellPosition.y, 0);
            currentTile = map.GetTile(clickCellPosition);
            swapTile(clickCellPosition, rotationRoads);
            swapTile(clickCellPosition, crossRoads);
            swapTile(clickCellPosition, Roads);

        }
    }
    private void swapTile(Vector3Int point,TileBase[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (currentTile.Equals(arr[i]))
                if (i == arr.Length-1)
                    map.SetTile(point, arr[0]);
                else
                    map.SetTile(point, arr[i + 1]);
        }
    }
}
