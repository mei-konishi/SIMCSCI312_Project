using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour {

    private int playerAtkPuzSolved;
    private int playerDefPuzSolved;
    private int enemyAtkPuzSolved;
    private int enemyDefPuzSolved;

    private int currentActivePuzzle; // 0 for atk, 1 for def, 2 for ulti

    private Text puzzleSolvedText;

    // Use this for initialization
    void Start () {
        playerAtkPuzSolved = 0;
        playerDefPuzSolved = 0;
        enemyAtkPuzSolved = 0;
        enemyDefPuzSolved = 0;
        currentActivePuzzle = 0;

        puzzleSolvedText = GameObject.Find("PuzzleSolvedText").GetComponent<Text>();
    }

    public void AtkPuzzleSolved()
    {
        playerAtkPuzSolved++;
    }

    public void DefPuzzleSolved()
    {
        playerDefPuzSolved++;
    }

    public void enemyAtkPuzzleSolved()
    {
        enemyAtkPuzSolved++;
    }

    public void EnemyDefPuzzleSolved()
    {
        enemyDefPuzSolved++;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
