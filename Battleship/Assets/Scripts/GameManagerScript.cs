using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public bool GameRunning = true;
    
    public int playerTurn = 1;
    public int turnPhase = 1;

    public List<GameObject> Player1Ships = new List<GameObject>();
    public List<GameObject> Player2Ships = new List<GameObject>();

    public Text victoryText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnPhase > 2 || turnPhase < 1) turnPhase = 1;
        if (playerTurn > 2 || playerTurn < 1) playerTurn = 1;

        if (Player1Ships.Count <= 0 && GameRunning)
        {
            victoryText.text ="Player 2 Wins!";
            victoryText.gameObject.SetActive(true);
            //Time.timeScale = 0f;
            GameRunning = false;
        }
        else if (Player2Ships.Count <= 0 && GameRunning)
        {
            victoryText.text ="Player 1 Wins!";
            victoryText.gameObject.SetActive(true);
            //Time.timeScale = 0f;
            GameRunning = false;
        }
    }
}
