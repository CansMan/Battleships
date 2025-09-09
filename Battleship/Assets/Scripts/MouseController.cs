using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class MouseController : MonoBehaviour
{
    public Camera cam;
    public Tilemap map;
    public GameManagerScript mgrObj;
    private GameObject heldShip;
    
    public TMP_Text shipInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = .1f;
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

        //Debug.DrawRay(mousePosition, Vector2.zero, Color.green);

        DisplayShipInfo(hits, mousePosition);

        if (Input.GetMouseButtonDown(0) && mgrObj.GameRunning == true)
        {
            if (mgrObj.turnPhase == 1)
            {
                ShipMove(hits, mousePosition);
            }
            else if (mgrObj.turnPhase == 2)
            {
                ShipFire(hits, mousePosition);
            }
        }
     }

    //Selects and moves a ship
    public void ShipMove(RaycastHit2D[] hits, Vector3 mousePosition)
    {
        bool isShip = false;
        bool isTile = false;
        bool isMap = false;

        GameObject ship = null;
        GameObject tileHit = null;


        if (hits.Length > 0)
        {
            foreach (RaycastHit2D col in hits)
            {
                if (col.collider.gameObject.layer == 6)
                {
                    isShip = true;
                    ship = col.collider.gameObject;
                }
                if (col.collider.gameObject.layer == 7)
                {
                    isTile = true;
                    tileHit = col.collider.gameObject;
                }
                if (col.collider.gameObject.layer == 3)
                {
                    isMap = true;
                }
            }
            if (isShip && heldShip == null && IsPlayerShip(mgrObj.playerTurn, ship))
            {
                heldShip = ship;
                heldShip.GetComponent<ShipScript>().ShowMoveTiles();
                heldShip.GetComponent<PolygonCollider2D>().enabled = false;
            }
            else if (isMap && isTile && heldShip != null)
            {
                if (CheckPath(mousePosition))
                {
                    Vector3Int tileMapPos = map.WorldToCell(mousePosition);
                    heldShip.GetComponent<ShipScript>().HideMoveTiles();
                    heldShip.transform.position = map.GetCellCenterWorld(tileMapPos);
                    heldShip.GetComponent<PolygonCollider2D>().enabled = true;
                    if (tileHit.tag == "LeftTurnTile")
                    {
                        heldShip.GetComponent<ShipScript>().rotation--;
                    }
                    if (tileHit.tag == "RightTurnTile")
                    {
                        heldShip.GetComponent<ShipScript>().rotation++;
                    }
                    //heldShip = null;
                    mgrObj.turnPhase++;
                    heldShip.GetComponent<ShipScript>().UpdateRotation();
                    ShipFire(hits, mousePosition);
                }
            }
            else if (isMap && !isTile && isShip && IsPlayerShip(mgrObj.playerTurn, ship))
            {
                heldShip.GetComponent<ShipScript>().HideMoveTiles();
                heldShip = null;
            }
        }
    }

    //Selects an enemy ship to fire on
    public void ShipFire(RaycastHit2D[] hits, Vector3 mousePosition)
    {
        bool isShip = false;
        bool isTile = false;
        bool isMap = false;
        GameObject tileHit = null;
        GameObject ship = null;

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D col in hits)
            {
                if (col.collider.gameObject.layer == 6)
                {
                    isShip = true;
                    ship = col.collider.gameObject;
                }
                if (col.collider.gameObject.layer == 8)
                {
                    isTile = true;
                    tileHit = col.collider.gameObject;
                }
                if (col.collider.gameObject.layer == 3)
                {
                    isMap = true;
                }
            }
            heldShip.GetComponent<ShipScript>().ShowFireTiles();
            if (isMap && isTile && isShip && !IsPlayerShip(mgrObj.playerTurn, ship))
            {
                ship.GetComponent<ShipScript>().hullInt--;
                
                heldShip.GetComponent<ShipScript>().HideFireTiles();
                heldShip = null;
                mgrObj.turnPhase++;
                mgrObj.playerTurn++;
            } 
            else if (isMap && !isTile && isShip && IsPlayerShip(mgrObj.playerTurn, ship))
            {
                heldShip.GetComponent<ShipScript>().HideFireTiles();
                heldShip = null;
                mgrObj.turnPhase++;
                mgrObj.playerTurn++;
            }
            //Debug.Log(mgrObj.playerTurn);
        }
    }

    public bool CheckPath(Vector3 mousePosition)
    {
        Vector3Int tileMapPos = map.WorldToCell(mousePosition);
        RaycastHit2D[] pathHits = Physics2D.LinecastAll(heldShip.transform.position, map.GetCellCenterWorld(tileMapPos));
        foreach (RaycastHit2D hit in pathHits)
        {
            if (hit.collider.gameObject.layer == 6 && hit.collider.gameObject != heldShip)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsPlayerShip(int player, GameObject ship)
    {
        if (player == ship.GetComponent<ShipScript>().playerSide)
        {
            return true;
        }
        return false;
    }

    public void DisplayShipInfo(RaycastHit2D[] hits, Vector3 mousePos)
    {
        bool isShip = false;
        GameObject ship = null;
        
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D col in hits)
            {
                if (col.collider.gameObject.layer == 6)
                {
                    isShip = true;
                    ship = col.collider.gameObject;
                }
            }
        }

        if (isShip)
        {
            string shipType = "";
            int hullMax = 0;
            int hullInt = 0;

            switch (ship.GetComponent<ShipScript>().shipType)
            {
                case 1:
                    shipType = "Scout Ship";
                    hullMax = 1;
                    break;
                case 2:
                    shipType = "Battleship";
                    hullMax = 3;
                    break;
                case 3:
                case 4:
                default:
                    break;
            }

            hullInt = ship.GetComponent<ShipScript>().hullInt;

            shipInfo.text = "Ship Type: " + shipType + "\nHull Integrity: " + hullInt + "/" + hullMax;
            //shipInfo.ForceMeshUpdate();
            shipInfo.gameObject.SetActive(true);
        }
        else
        {
            shipInfo.gameObject.SetActive(false);
        }
    }
}

