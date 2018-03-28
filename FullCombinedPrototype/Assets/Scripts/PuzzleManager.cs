using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour {

    public static PuzzleManager instance = null; // Static instance of Puzzle Manager

    private static int playerAtkPuzSolved;
    private static int playerDefPuzSolved;
    private int enemyAtkPuzSolved;
    private int enemyDefPuzSolved;

    private int currentActivePuzzle; // 0 for atk, 1 for def, 2 for ulti

    private Text puzzleSolvedText;

    // Use this for initialization
    void Awake () {
        
        if (instance == null){ // check if instance already exists
            instance = this;    // if not, set instance to this
        }

        else if (instance != this){ // if instance already exists and is not this
            Destroy(gameObject);    // then destroy it. enforcing singleton
        }

        DontDestroyOnLoad(gameObject); // don't destroy when reloading scene (need? or nah?)

        playerAtkPuzSolved = 0;
        playerDefPuzSolved = 0;
        enemyAtkPuzSolved = 0;
        enemyDefPuzSolved = 0;
        currentActivePuzzle = 0;

        puzzleSolvedText = GameObject.Find("PuzzleSolvedText").GetComponent<Text>();
        
    }

    // call this function when atk puzzle is solved!
    public static void AtkPuzzleSolved()
    {
        playerAtkPuzSolved++;
    }
    
    // call this function when def puzzle is solved!
    public static void DefPuzzleSolved()
    {
        playerDefPuzSolved++;
    }

    // for AI use.
    public void enemyAtkPuzzleSolved()
    {
        enemyAtkPuzSolved++;
    }

    // for AI use.
    public void EnemyDefPuzzleSolved()
    {
        enemyDefPuzSolved++;
    }

    public void resetScore()
    {
        playerAtkPuzSolved = 0;
        playerDefPuzSolved = 0;
        enemyAtkPuzSolved = 0;
        enemyDefPuzSolved = 0;
    }

    // temp AI, will change next time to real time update
    public void enemyAI()
    {
        enemyAtkPuzSolved = Random.Range(5, 10);
        enemyDefPuzSolved = Random.Range(0, 3);
    }

    // getters ===============================================
    public int getPlayerAtkScore()
    {
        return playerAtkPuzSolved;
    }

    public int getPlayerDefScore()
    {
        return playerDefPuzSolved;
    }

    public int getEnemyAtkScore()
    {
        return enemyAtkPuzSolved;
    }

    public int getEnemyDefScore()
    {
        return enemyDefPuzSolved;
    }
	
	// Update is called once per frame =============================
	void Update () {
        // during puzzle phase
        if (Timer.checkPuzzlePhase())
        {
            // For prototype: using keyboard input as "puzzle solved"
            if (Input.GetKeyDown("a"))
            {
                playerAtkPuzSolved++;
            }
            if (Input.GetKeyDown("d"))
            {
                playerDefPuzSolved++;
            }
        }

        puzzleSolvedText.text = "Atk Puzzles Solved: " + playerAtkPuzSolved + "\n"
                                + "Def Puzzles Solved: " + playerDefPuzSolved;

    }
}
