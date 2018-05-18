using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance = null; // Static instance of Puzzle Manager

    private const int MAX_PUZ_SOLVE = 6;

    private static int playerAtkPuzSolved;
    private static int playerDefPuzSolved;
    private static bool playerUltiPuzSolved;
    private int enemyAtkPuzSolved;
    private int enemyDefPuzSolved;

    private StatsUIManager statsUIManagerScript;

    private static int currentActivePuzzle; // 1 for atk, 2 for def, 3 for ulti

    public GameObject[] puzzleObjects; // this holds all puzzle objects

    public static PuzzleControllerInterface[] puzzleControllers; // this holds all puzzles
    public static PuzzleControllerInterface[] slottedPuzzleCtrls; // this holds selected skills


    // Use this for initialization
    void Awake()
    {
        if (instance == null) // check if instance already exists
        { 
            instance = this;    // if not, set instance to this
        }

        else if (instance != this) // if instance already exists and is not this
        { 
            Destroy(gameObject);    // then destroy it. enforcing singleton
        }

        playerAtkPuzSolved = 0;
        playerDefPuzSolved = 0;
        playerUltiPuzSolved = false;
        enemyAtkPuzSolved = 0;
        enemyDefPuzSolved = 0;
        currentActivePuzzle = 1;
        
        Instantiate(puzzleObjects[0], new Vector3(0f, 0f, 0f), Quaternion.identity);
        Instantiate(puzzleObjects[1], new Vector3(0f, 0f, 0f), Quaternion.identity);

        // get the stats UI manager script and set up the UI
        statsUIManagerScript = GetComponent<StatsUIManager>();
        statsUIManagerScript.Setup();

        // TESTING putting in different controllers into array of interface
        puzzleControllers = new PuzzleControllerInterface[2];
        puzzleControllers[0] = FindObjectOfType<SimonSaysGameController>();
        puzzleControllers[1] = FindObjectOfType<MemoryPuzzleController>();
        
        // IN THE FUTURE this will change to be a function?
        slottedPuzzleCtrls = new PuzzleControllerInterface[2];
        slottedPuzzleCtrls[0] = puzzleControllers[0];
        slottedPuzzleCtrls[1] = puzzleControllers[1];

        StartFirstPuzzle();
    }

    private void StartFirstPuzzle()
    {
        slottedPuzzleCtrls[0].Play();
    }

    // call this function when puzzle is solved!
    public void PuzzleSolved(int puzType)
    {
        switch (puzType)
        {
            case 1: if (playerAtkPuzSolved < MAX_PUZ_SOLVE) // if max not reached
                { 
                    playerAtkPuzSolved++;   // increment attack puzzle count
                    statsUIManagerScript.UpdateAtkPuzzleSolved(playerAtkPuzSolved); // update UI
                }
                break;
            case 2: if (playerDefPuzSolved < MAX_PUZ_SOLVE)// if max not reached
                { 
                    playerDefPuzSolved++; // increment defence puzzle count
                    statsUIManagerScript.UpdateDefPuzzleSolved(playerDefPuzSolved); // update UI
                }
                break;
            case 3: playerUltiPuzSolved = true;
                break;
        }
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
        statsUIManagerScript.ClearPoints(); // clear UI
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
                case 1:
                    slottedPuzzleCtrls[0].Play();
                    slottedPuzzleCtrls[1].Stop();
                    break;
                case 2:
                    slottedPuzzleCtrls[0].Stop();
                    slottedPuzzleCtrls[1].Play();
                    break;
                case 3:
                    slottedPuzzleCtrls[0].Stop();
                    slottedPuzzleCtrls[1].Stop();
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
        
    }

    // check current active puzzle being selected, and make sure the layer is being set right
    private void UpdateActivePuzzleSelection(int newActivePuzzle)
    {
        // get the current active bg and objects and new active bg and objects
        GameObject currentBackground = null;
        GameObject[] currentObjects = null;
        GameObject newActiveBackground = null;
        GameObject[] newActiveObjects = null;

        // grab all current active puzzle objects 
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

        // grab all new active puzzle objects
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

        // now switch their display layers and z pos (to compensate for stacked colliders)
        currentBackground.GetComponent<Renderer>().sortingLayerName = "BackgroundPuzzles";
        foreach (GameObject currObj in currentObjects)
        {
            currObj.GetComponent<Renderer>().sortingLayerName = "BackgroundPuzzles";
            Vector3 originalPosition = currObj.GetComponent<Transform>().position;
            Vector3 newPosition = originalPosition + new Vector3(0, 0, 0.1f); // move the object backwards
            currObj.GetComponent<Transform>().position = newPosition;
        }

        newActiveBackground.GetComponent<Renderer>().sortingLayerName = "ForegroundPuzzle";
        foreach (GameObject currObj in newActiveObjects)
        {
            currObj.GetComponent<Renderer>().sortingLayerName = "ForegroundPuzzleObj";
            Vector3 originalPosition = currObj.GetComponent<Transform>().position;
            Vector3 newPosition = originalPosition + new Vector3(0, 0, -0.1f); // move the object backwards
            currObj.GetComponent<Transform>().position = newPosition;
        }
    }
}


