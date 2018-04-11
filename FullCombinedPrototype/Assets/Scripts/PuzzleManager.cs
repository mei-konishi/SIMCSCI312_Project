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
    
    private static int currentActivePuzzle; // 1 for atk, 2 for def, 3 for ulti

    private Text puzzleSolvedText;

    public GameObject[] puzzleObjects; // this holds all puzzle objects

    public static PuzzleControllerInterface[] puzzleControllers; // this holds all puzzles
    public static PuzzleControllerInterface[] slottedPuzzleCtrls; // this holds selected skills

    
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

        playerAtkPuzSolved = 0;
        playerDefPuzSolved = 0;
        enemyAtkPuzSolved = 0;
        enemyDefPuzSolved = 0;
        currentActivePuzzle = 1;

        puzzleSolvedText = GameObject.Find("PuzzleSolvedText").GetComponent<Text>();
        Instantiate(puzzleObjects[1], new Vector3(2.19f, -2.65f, 0.05f), Quaternion.identity);

        // TESTING putting in different controllers into array of interface
        puzzleControllers = new PuzzleControllerInterface[2];
        puzzleControllers[0] = FindObjectOfType<SimonSaysGameController>();
        puzzleControllers[1] = FindObjectOfType<MemoryPuzzleController>();

        // IN THE FUTURE this will change to be a fun
        slottedPuzzleCtrls = new PuzzleControllerInterface[2];
        slottedPuzzleCtrls[0] = puzzleControllers[0];
        slottedPuzzleCtrls[1] = puzzleControllers[1];
        

        StartFirstPuzzle();
    }

    private void StartFirstPuzzle()
    {
        slottedPuzzleCtrls[0].Play(); // set the first puzzle to active
        puzzleObjects[1].SetActive(false);  // set the objects of second puzzle to deactivate
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

            // activate and deactivate puzzles 
            switch (puzzleNumber)
            {
                case 1: slottedPuzzleCtrls[0].Play(); // activate puzzle
                    //puzzleObjects[0].SetActive(true); // activate objects
                    slottedPuzzleCtrls[1].Stop(); // deactivate puzzles
                    puzzleObjects[1].SetActive(false); // hide objects
                    break;
                case 2: slottedPuzzleCtrls[0].Stop();
                    puzzleObjects[0].SetActive(false);
                    slottedPuzzleCtrls[1].Play();
                    puzzleObjects[1].SetActive(true);
                    break;
                case 3: slottedPuzzleCtrls[0].Stop();
                    puzzleObjects[0].SetActive(false);
                    slottedPuzzleCtrls[1].Stop();
                    puzzleObjects[1].SetActive(true);
                    break;
            }
        }
    }

    // function to stop puzzles 
    public static void StopPuzzle()
    {
        slottedPuzzleCtrls[currentActivePuzzle - 1].Stop();
    }

    // function to start puzzles
    public static void StartPuzzle()
    {
        slottedPuzzleCtrls[currentActivePuzzle - 1].Play();
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
            case 1:
                currentBackground = GameObject.FindGameObjectWithTag("P1Bg");
                currentObjects = GameObject.FindGameObjectsWithTag("P1Obj");
                break;
                
            case 2:
                currentBackground = GameObject.FindGameObjectWithTag("P2Bg");
                currentObjects = GameObject.FindGameObjectsWithTag("P2Obj");
                break;

            case 3:
                currentBackground = GameObject.FindGameObjectWithTag("P3Bg");
                currentObjects = GameObject.FindGameObjectsWithTag("P3Obj");
                break;
        }

        switch (newActivePuzzle)
        {

            case 1:
                newActiveBackground = GameObject.FindGameObjectWithTag("P1Bg");
                newActiveObjects = GameObject.FindGameObjectsWithTag("P1Obj");
                break;

            case 2:
                newActiveBackground = GameObject.FindGameObjectWithTag("P2Bg");
                newActiveObjects = GameObject.FindGameObjectsWithTag("P2Obj");
                break;

            case 3:
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
            currObj.GetComponent<Renderer>().sortingLayerName = "ForegroundPuzzleObj";
        }
    }
}


