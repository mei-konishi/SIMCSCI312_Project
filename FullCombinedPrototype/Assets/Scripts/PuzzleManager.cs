﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{

    public static PuzzleManager instance = null; // Static instance of Puzzle Manager

    private static int playerAtkPuzSolved;
    private static int playerDefPuzSolved;
    private int enemyAtkPuzSolved;
    private int enemyDefPuzSolved;

    private static int currentActivePuzzle; // 0 for atk, 1 for def, 2 for ulti

    private Text puzzleSolvedText;

    // Use this for initialization
    void Awake()
    {

        if (instance == null)
        { // check if instance already exists
            instance = this;    // if not, set instance to this
        }

        else if (instance != this)
        { // if instance already exists and is not this
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
    public void EnemyAtkPuzzleSolved()
    {
        enemyAtkPuzSolved++;
    }

    // for AI use.
    public void EnemyDefPuzzleSolved()
    {
        enemyDefPuzSolved++;
    }

    public void ResetScore()
    {
        playerAtkPuzSolved = 0;
        playerDefPuzSolved = 0;
        enemyAtkPuzSolved = 0;
        enemyDefPuzSolved = 0;
    }

    // temp AI, will change next time to real time update
    public void EnemyAI()
    {
        enemyAtkPuzSolved = Random.Range(5, 10);
        enemyDefPuzSolved = Random.Range(0, 3);
    }

    // getters ===============================================
    public int GetPlayerAtkScore()
    {
        return playerAtkPuzSolved;
    }

    public int GetPlayerDefScore()
    {
        return playerDefPuzSolved;
    }

    public int GetEnemyAtkScore()
    {
        return enemyAtkPuzSolved;
    }

    public int GetEnemyDefScore()
    {
        return enemyDefPuzSolved;
    }

    public int GetCurrentPuzzle()
    {
        return currentActivePuzzle;
    }

    // setter ======================================================
    public void SetNewActive(int puzzleNumber)
    {
        // if new input is different from current
        if (puzzleNumber != currentActivePuzzle)
        {
            // swap their display layers
            UpdateActivePuzzleSelection(puzzleNumber);
            // update current active puzzle
            currentActivePuzzle = puzzleNumber;
        }
    }

    // Update is called once per frame =============================
    void Update()
    {
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

    // check current active puzzle being selected, and make sure the layer is being set right
    private void UpdateActivePuzzleSelection(int newActivePuzzle)
    {
        // get the current active bg and objects and new active bg and objects
        GameObject currentBackground = null;
        GameObject[] currentObjects = null;
        GameObject newActiveBackground = null;
        GameObject[] newActiveObjects = null;

        switch (currentActivePuzzle)
        {
            case 0:
                currentBackground = GameObject.FindGameObjectWithTag("P1Bg");
                currentObjects = GameObject.FindGameObjectsWithTag("P1Obj");
                break;

            case 1:
                currentBackground = GameObject.FindGameObjectWithTag("P2Bg");
                currentObjects = GameObject.FindGameObjectsWithTag("P2ObjCard");
                break;

            case 2:
                currentBackground = GameObject.FindGameObjectWithTag("P3Bg");
                currentObjects = GameObject.FindGameObjectsWithTag("P3Obj");
                break;
        }

        switch (newActivePuzzle)
        {
            case 0:
                newActiveBackground = GameObject.FindGameObjectWithTag("P1Bg");
                newActiveObjects = GameObject.FindGameObjectsWithTag("P1Obj");
                break;

            case 1:
                newActiveBackground = GameObject.FindGameObjectWithTag("P2Bg");
                newActiveObjects = GameObject.FindGameObjectsWithTag("P2ObjCard");
                break;

            case 2:
                newActiveBackground = GameObject.FindGameObjectWithTag("P3Bg");
                newActiveObjects = GameObject.FindGameObjectsWithTag("P3Obj");
                break;
        }

        // now switch their display layers 
        currentBackground.GetComponent<Renderer>().sortingLayerName = "BackgroundPuzzles";
        foreach (GameObject currObj in currentObjects)
        {
            currObj.GetComponent<Renderer>().sortingLayerName = "BackgroundPuzzles";
        }

        newActiveBackground.GetComponent<Renderer>().sortingLayerName = "ForegroundPuzzle";
        foreach (GameObject currObj in newActiveObjects)
        {
            currObj.GetComponent<Renderer>().sortingLayerName = "ForegroundPuzzle";
        }
    }
}


