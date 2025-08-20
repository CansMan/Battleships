using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public int hullInt = 0;
    public int shipType = 1;
    public int rotation = 0;
    public int playerSide = 1;

    public Sprite UpSprite;
    public Sprite RightSprite;
    public Sprite DownSprite;
    public Sprite LeftSprite;

    public GameObject MoveTilesUp;
    public GameObject MoveTilesLeft;
    public GameObject MoveTilesDown;
    public GameObject MoveTilesRight;

    public GameObject FireTilesUp;
    public GameObject FireTilesLeft;
    public GameObject FireTilesDown;
    public GameObject FireTilesRight;

    public Collider2D UpDownCollider;
    public Collider2D LeftRightCollider;

    GameObject MgrObject;

    // Start is called before the first frame update
    void Start()
    {
        MgrObject = FindObjectOfType<GameManagerScript>().gameObject;
        if (playerSide == 1) MgrObject.GetComponent<GameManagerScript>().Player1Ships.Add(this.gameObject);
        else if (playerSide == 2) MgrObject.GetComponent<GameManagerScript>().Player2Ships.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        if (hullInt <= 0)
        {
            if (playerSide == 1) MgrObject.GetComponent<GameManagerScript>().Player1Ships.RemoveAt(MgrObject.GetComponent<GameManagerScript>().Player1Ships.IndexOf(this.gameObject));
            else if (playerSide == 2) MgrObject.GetComponent<GameManagerScript>().Player2Ships.RemoveAt(MgrObject.GetComponent<GameManagerScript>().Player2Ships.IndexOf(this.gameObject));
            Destroy(this.gameObject);
        }
    }

    public void ShowMoveTiles()
    {
        switch (rotation)
        {
            case 0:
                MoveTilesUp.SetActive(true);
                for (int i = 0; i < MoveTilesUp.transform.childCount; i++)
                {
                    Collider2D[] scans = Physics2D.OverlapCircleAll(MoveTilesUp.transform.GetChild(i).position, 0.1f);
                    foreach (Collider2D scan in scans)
                    {
                        if (scan.gameObject.layer == 3 && CheckPath(MoveTilesUp.transform.GetChild(i).position))
                        {
                            MoveTilesUp.transform.GetChild(i).GetComponent<OverlayTile>().ShowTile();
                        }
                    }
                }
                break;
            case 1:
                MoveTilesRight.SetActive(true);
                for (int i = 0; i < MoveTilesRight.transform.childCount; i++)
                {
                    Collider2D[] scans = Physics2D.OverlapCircleAll(MoveTilesRight.transform.GetChild(i).position, 0.1f);
                    foreach (Collider2D scan in scans)
                    {
                        if (scan.gameObject.layer == 3 && CheckPath(MoveTilesRight.transform.GetChild(i).position))
                        {
                            MoveTilesRight.transform.GetChild(i).GetComponent<OverlayTile>().ShowTile();
                        }
                    }
                }
                break;
            case 2:
                MoveTilesDown.SetActive(true);
                for (int i = 0; i < MoveTilesDown.transform.childCount; i++)
                {
                    Collider2D[] scans = Physics2D.OverlapCircleAll(MoveTilesDown.transform.GetChild(i).position, 0.1f);
                    foreach (Collider2D scan in scans)
                    {
                        if (scan.gameObject.layer == 3 && CheckPath(MoveTilesDown.transform.GetChild(i).position))
                        {
                            MoveTilesDown.transform.GetChild(i).GetComponent<OverlayTile>().ShowTile();
                        }
                    }
                }
                break;
            case 3:
                MoveTilesLeft.SetActive(true);
                for (int i = 0; i < MoveTilesLeft.transform.childCount; i++)
                {
                    Collider2D[] scans = Physics2D.OverlapCircleAll(MoveTilesLeft.transform.GetChild(i).position, 0.1f);
                    foreach (Collider2D scan in scans)
                    {
                        if (scan.gameObject.layer == 3 && CheckPath(MoveTilesLeft.transform.GetChild(i).position))
                        {
                            MoveTilesLeft.transform.GetChild(i).GetComponent<OverlayTile>().ShowTile();
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    public void HideMoveTiles()
    {
        switch (rotation)
        {
            case 0:
                MoveTilesUp.SetActive(false);
                for (int i = 0; i < MoveTilesUp.transform.childCount; i++)
                {
                    MoveTilesUp.transform.GetChild(i).GetComponent<OverlayTile>().HideTile();
                }
                break;
            case 1:
                MoveTilesRight.SetActive(false);
                for (int i = 0; i < MoveTilesRight.transform.childCount; i++)
                {
                    MoveTilesRight.transform.GetChild(i).GetComponent<OverlayTile>().HideTile();
                }
                break;
            case 2:
                MoveTilesDown.SetActive(false);
                for (int i = 0; i < MoveTilesDown.transform.childCount; i++)
                {
                    MoveTilesDown.transform.GetChild(i).GetComponent<OverlayTile>().HideTile();
                }
                break;
            case 3:
                MoveTilesLeft.SetActive(false);
                for (int i = 0; i < MoveTilesLeft.transform.childCount; i++)
                {
                    MoveTilesLeft.transform.GetChild(i).GetComponent<OverlayTile>().HideTile();
                }
                break;
            default:
                break;
        }
    }

    public void ShowFireTiles()
    {
        switch (rotation)
        {
            case 0:
                FireTilesUp.SetActive(true);
                for (int i = 0; i < FireTilesUp.transform.childCount; i++)
                {
                    Collider2D[] scans = Physics2D.OverlapCircleAll(FireTilesUp.transform.GetChild(i).position, 0.1f);
                    foreach (Collider2D scan in scans)
                    {
                        if (scan.gameObject.layer == 3)
                        {
                            FireTilesUp.transform.GetChild(i).GetComponent<OverlayTile>().ShowTile();
                        }
                    }
                }
                break;
            case 1:
                FireTilesRight.SetActive(true);
                for (int i = 0; i < FireTilesRight.transform.childCount; i++)
                {
                    Collider2D[] scans = Physics2D.OverlapCircleAll(FireTilesRight.transform.GetChild(i).position, 0.1f);
                    foreach (Collider2D scan in scans)
                    {
                        if (scan.gameObject.layer == 3)
                        {
                            FireTilesRight.transform.GetChild(i).GetComponent<OverlayTile>().ShowTile();
                        }
                    }
                }
                break;
            case 2:
                FireTilesDown.SetActive(true);
                for (int i = 0; i < FireTilesDown.transform.childCount; i++)
                {
                    Collider2D[] scans = Physics2D.OverlapCircleAll(FireTilesDown.transform.GetChild(i).position, 0.1f);
                    foreach (Collider2D scan in scans)
                    {
                        if (scan.gameObject.layer == 3)
                        {
                            FireTilesDown.transform.GetChild(i).GetComponent<OverlayTile>().ShowTile();
                        }
                    }
                }
                break;
            case 3:
                FireTilesLeft.SetActive(true);
                for (int i = 0; i < FireTilesLeft.transform.childCount; i++)
                {
                    Collider2D[] scans = Physics2D.OverlapCircleAll(FireTilesLeft.transform.GetChild(i).position, 0.1f);
                    foreach (Collider2D scan in scans)
                    {
                        if (scan.gameObject.layer == 3)
                        {
                            FireTilesLeft.transform.GetChild(i).GetComponent<OverlayTile>().ShowTile();
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    public void HideFireTiles()
    {
        switch (rotation)
        {
            case 0:
                FireTilesUp.SetActive(false);
                for (int i = 0; i < FireTilesUp.transform.childCount; i++)
                {
                    FireTilesUp.transform.GetChild(i).GetComponent<OverlayTile>().HideTile();
                }
                break;
            case 1:
                FireTilesRight.SetActive(false);
                for (int i = 0; i < FireTilesRight.transform.childCount; i++)
                {
                    FireTilesRight.transform.GetChild(i).GetComponent<OverlayTile>().HideTile();
                }
                break;
            case 2:
                FireTilesDown.SetActive(false);
                for (int i = 0; i < FireTilesDown.transform.childCount; i++)
                {
                    FireTilesDown.transform.GetChild(i).GetComponent<OverlayTile>().HideTile();
                }
                break;
            case 3:
                FireTilesLeft.SetActive(false);
                for (int i = 0; i < FireTilesLeft.transform.childCount; i++)
                {
                    FireTilesLeft.transform.GetChild(i).GetComponent<OverlayTile>().HideTile();
                }
                break;
            default:
                break;
        }
    }
    public bool CheckPath(Vector3 cellPos)
    {
        RaycastHit2D[] pathHits = Physics2D.LinecastAll(transform.position, cellPos);
        foreach (RaycastHit2D hit in pathHits)
        {
            if (hit.collider.gameObject.layer == 6 && hit.collider.gameObject != this.gameObject)
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateRotation()
    {
        if (rotation > 3)
        {
            rotation = 0;
        }
        else if (rotation < 0)
        {
            rotation = 3;
        }
        switch (rotation)
        {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().sprite = UpSprite;
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = RightSprite;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = DownSprite;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = LeftSprite;
                break;
            default:
                break;
        }

        if (shipType != 1)
        {
            switch (rotation)
            {
                case 0:
                case 2:
                    UpDownCollider.enabled = true;
                    LeftRightCollider.enabled = false;
                    break;
                case 1:
                case 3:
                    LeftRightCollider.enabled = true;
                    UpDownCollider.enabled = false;
                    break;
            }
        }
    }
}