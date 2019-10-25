using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class TilemapContorller : MonoBehaviour
{
    public Text txt;
    public TileBase TileToSet;
    public Tilemap map;
    private Camera mainCamera;
    public Transform Player;
    private TileBase currentTile;
    public TileBase[] rotationRoads;
    public TileBase[] crossRoads;
    public TileBase[] Roads;
    public TileBase[] decoration;
    private Vector3Int clickCellPositionRight;
    private Vector3 clicWorldPositionRight;
    private Vector3 moveTo;
    private BoundsInt area;
    void Start()
    {
        //smap = GetComponent<Tilemap>();
        mainCamera = Camera.main;
        clickCellPositionRight = new Vector3Int( 0,0,0);
        area = map.cellBounds;
        
       
    }
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
        if (Input.GetMouseButtonDown(1))
        {
            clicWorldPositionRight = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            clickCellPositionRight = map.WorldToCell(clicWorldPositionRight);
            clickCellPositionRight = new Vector3Int(clickCellPositionRight.x, clickCellPositionRight.y, 0);
        }
        moveTo = map.CellToWorld(clickCellPositionRight);
        moveTo = new Vector3(moveTo.x, moveTo.y + 0.25f, moveTo.z);
        Player.position = Vector3.MoveTowards(Player.position, moveTo, Time.deltaTime);
        if (Input.GetMouseButtonDown(2))
        {
            TileBase[] tileArray = map.GetTilesBlock(area);
            Debug.Log(area);
            Debug.Log(tileArray.Length);
            txt.text += "\n";
            for (int i = 0; i < tileArray.Length; i++)
            {
                if (tileArray[i])
                {
                    txt.text += "tl=" + i + "\t" + tileArray[i].name.Remove(0, 10) + "   ";
                    if (i % 10 == 0)
                        txt.text += "\n";
                }
                else { 
                txt.text += "tl=" + i + "\t" + "NULL  " ;
                if (i % 10 == 0)
                        txt.text += "\n";
                }
            }
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
//transform.position = Vector3.MoveTowards(Pos1, Pos2, Time.deltaTime*2);