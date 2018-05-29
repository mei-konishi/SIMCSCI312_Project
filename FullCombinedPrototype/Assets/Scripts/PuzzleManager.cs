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
    private static int enemyAtkPuzSolved;
    private static int enemyDefPuzSolved;

    private StatsUIManager statsUIManagerScript;

    private int[] puzzles; // an array of 3 int, with each int telling us which puzzle is to be loaded
    private int noOfPuzzles; // whether there's 2 or 3 puzzles
    private static int currentActivePuzzle; // 1 for atk, 2 for def, 3 for ulti

    public GameObject[] puzzleObjects; // this holds all puzzle objects

    public static PuzzleControllerInterface[] puzzleControllers; // this holds the puzzle controllers 

    // HAAAAAX. As in hacks :3
    public bool hack_Ulti_On;
    public bool hack_init_tutorials;
    public bool hack_init_everything;

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

        checkHacks(); // check if any hacks on, and update accordingly

        // go to player prefs to get skills equipped information,
        // and pass into function to turn the string into array of int to find puzzles equipped
        puzzles = new int[3];
        Formulas.StringToIntArray(PlayerPrefs.GetString("skillsEquipped"), puzzles);
        // get number of puzzles
        if (puzzles[2] == 0)
            noOfPuzzles = 2;
        else
            noOfPuzzles = 3;

        // Instantiate the puzzles we need
        InstantiatePuzzles();

        // get the stats UI manager script and set up the UI
        statsUIManagerScript = GetComponent<StatsUIManager>();
        statsUIManagerScript.Setup();

        // get the controllers of each puzzle
        GetControllers();
        
    }

    private void InstantiatePuzzles()
    {
        // instantitate the puzzles according to the number in each array
        if (puzzles[0] == 1) // attack puzzle is first puzzle type
        {
            Instantiate(puzzleObjects[0], new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        else if (puzzles[0] == 2) // attack puzzle is second puzzle type
        {
            Instantiate(puzzleObjects[1], new Vector3(0f, 0f, 0f), Quaternion.identity);
        }

        if (puzzles[1] == 1) // defend puzzle is first puzzle type
        {
            Instantiate(puzzleObjects[2], new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        else if (puzzles[1] == 2) // defend puzzle is second puzzle type
        {
            Instantiate(puzzleObjects[3], new Vector3(0f, 0f, 0f), Quaternion.identity);
        }

        if (puzzles[2] == 1) // ultimate puzzle is available
        {
            Instantiate(puzzleObjects[4], new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
    }

    private void GetControllers()
    {
        // instantiate memory space for puzzle controllers
        puzzleControllers = new PuzzleControllerInterface[noOfPuzzles];

        // check level of puzzles 
        int[] puzzlesLevel = new int[3];
        Formulas.StringToIntArray(PlayerPrefs.GetString("equippedSkillsLevels"), puzzlesLevel);

        // get the controllers that are being created
        if (puzzles[0] == 1) // attack puzzle is first puzzle type
        {
            puzzleControllers[0] = FindObjectOfType<SimonSaysGameController>(); // load simon says
            puzzleControllers[0].SetDifficulty(puzzlesLevel[0]); // set the difficulty of the puzzle
        }
        else if (puzzles[0] == 2) // attack puzzle is second puzzle type
        {
            puzzleControllers[0] = FindObjectOfType<FollowTheLeaderController>(); // load follow the leader says
            puzzleControllers[0].SetDifficulty(puzzlesLevel[0]); // set the difficulty of the puzzle
        }

        if (puzzles[1] == 1) // defend puzzle is first puzzle type
        {
            puzzleControllers[1] = FindObjectOfType<MemoryPuzzleController>(); // load memory puzzle
            puzzleControllers[1].SetDifficulty(puzzlesLevel[1]); // set the difficulty of the puzzle
        }
        else if (puzzles[1] == 2) // defend puzzle is second puzzle type
        {
            // load your new puzzle controller here 
        }

        if (puzzles[2] == 1) // ultimate puzzle is available
        {
            puzzleControllers[2] = FindObjectOfType<UltimatePuzzleController>(); // load ultimate puzzle
            puzzleControllers[2].SetDifficulty(puzzlesLevel[2]); // set the difficulty of the puzzle
        }


    }

    private void StartFirstPuzzle()
    {
        puzzleControllers[0].Play();
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
        statsUIManagerScript.UpdateEnemyAtkPuzSolved(enemyAtkPuzSolved);
    }

    // for AI use.
    public void EnemyDefPuzzleSolved()
    {
        enemyDefPuzSolved++;
        statsUIManagerScript.UpdateEnemyDefPuzSolved(enemyDefPuzSolved);
    }

    public void ResetScore()
    {
        playerAtkPuzSolved = 0;
        playerDefPuzSolved = 0;
        enemyAtkPuzSolved = 0;
        enemyDefPuzSolved = 0;
        statsUIManagerScript.ClearPoints(); // clear UI
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
                    puzzleControllers[0].Play();
                    puzzleControllers[1].Stop();
                    if (noOfPuzzles == 3)
                        puzzleControllers[2].Stop();
                    break;
                case 2:
                    puzzleControllers[0].Stop();
                    puzzleControllers[1].Play();
                    if (noOfPuzzles == 3)
                        puzzleControllers[2].Stop();
                    break;
                case 3:
                    puzzleControllers[0].Stop();
                    puzzleControllers[1].Stop();
                    if (noOfPuzzles == 3)
                        puzzleControllers[2].Play();
                    break;
            }
        }
    }

    // function to stop puzzles 
    public static void StopPuzzle()
    {
        puzzleControllers[currentActivePuzzle - 1].Stop();
    }

    // function to start puzzles
    public static void StartPuzzle()
    {
        puzzleControllers[currentActivePuzzle - 1].Play();
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
            Vector3 newPosition = originalPosition + new Vector3(0, 0, 0.9f); // move the object backwards
            currObj.GetComponent<Transform>().position = newPosition;
        }

        newActiveBackground.GetComponent<Renderer>().sortingLayerName = "ForegroundPuzzle";
        foreach (GameObject currObj in newActiveObjects)
        {
            currObj.GetComponent<Renderer>().sortingLayerName = "ForegroundPuzzleObj";
            Vector3 originalPosition = currObj.GetComponent<Transform>().position;
            Vector3 newPosition = originalPosition + new Vector3(0, 0, -0.9f); // move the object backwards
            currObj.GetComponent<Transform>().position = newPosition;
        }
    }

    private void checkHacks()
    {
        if (hack_Ulti_On) // turn on ultimate skill (Available to play)
        {
            int[] skills = new int[3];
            // get playerPrefs current skill sets
            Formulas.StringToIntArray(PlayerPrefs.GetString("skillsEquipped"), skills);
            skills[2] = 1; // set ultimate to on
            // change back numbers to string
            string skillsString = Formulas.IntArrayToString(skills);
            // set back the numbers to playerPrefs
            PlayerPrefs.SetString("skillsEquipped", skillsString);
        }

        if (hack_init_tutorials) // initialize tutorials 
        {
            PlayerPrefs.SetInt("tutorial", 0);
        }

        if (hack_init_everything) // initialize all stats
        {
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("exp", 0);
            PlayerPrefs.SetInt("str", 1);
            PlayerPrefs.SetInt("def", 1);
            PlayerPrefs.SetInt("hp", 100);
            PlayerPrefs.SetInt("statsPoints", 0);
            PlayerPrefs.SetInt("skillPoints", 0);
            PlayerPrefs.SetString("skillsLevelsUnlocked", "11000");
            PlayerPrefs.SetString("skillsEquipped", "110");
            PlayerPrefs.SetString("equippedSkillsLevels", "110");
            PlayerPrefs.SetInt("stageUnlocked", 1);
            PlayerPrefs.SetInt("stageLastPlayed", 0);
            PlayerPrefs.SetInt("tutorial", 0);
        }
    }
}


