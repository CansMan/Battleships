using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap Map;
    public Dictionary<Vector2Int, Tile> mapArray;

    public OverlayTile overlayPrefab;
    public GameObject overlayContainer;

    // Start is called before the first frame update
    void Start()
    {
        Map.CompressBounds();
        BoundsInt bounds = Map.cellBounds;
        //Debug.Log(bounds);
        int cells = 0;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int z = bounds.min.z; z < bounds.max.z; z++)
                {

                    Map.GetTile(new Vector3Int(x, y, z));
                    Map.SetTileFlags(new Vector3Int(x, y, z), TileFlags.None);
                    cells++;

                    /*var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                    var cellWorldPos = Map.GetCellCenterWorld(new Vector3Int(x, y, z));
                    Debug.Log(cellWorldPos);
                    overlayTile.transform.position = new Vector3(cellWorldPos.x, cellWorldPos.y, cellWorldPos.z + 0.1f);
                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = Map.GetComponent<TilemapRenderer>().sortingOrder;*/

                    if ((x % 2 == 0 && y % 2 != 0)|| (y % 2 == 0 && x % 2 != 0))
                    {
                        
                        //Map.SetColor(new Vector3Int(x, y, z), Color.red);
                        //Debug.Log(Map.GetColor(new Vector3Int(x, y, z)));
                    }
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
