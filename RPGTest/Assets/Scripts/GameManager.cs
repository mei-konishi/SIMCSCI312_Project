using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.
    private BoardManager boardScript;           // create background

 //   public Timer timer;
    private static int level = 1;      // used to keep track of stage 
    private bool puzzlePhase;
    private List<Enemy> enemies; // testing. Following tutorial setup first. 
                                 // and maybe in the future can have a stream of monsters? 

    const float timerDuration = 10.0f;
    // temp timer for rpg testing
    float timeLeft;
    public Text timerText;
    public float turnDelay = 2f;

    // temp counter for number of puzzles solved
    private int atkPuzSolved;
    private int defPuzSolved;

    public Text puzzleSolvedText;

    // Use this for initialization
    void Awake () {

		if (instance == null){ // check if instance already exists
            instance = this;    // if not, set instance to this
        }

        else if (instance != this){ // if instance already exists and is not this
            Destroy(gameObject);    // then destroy it. enforcing singleton
        }

        DontDestroyOnLoad(gameObject); // don't destroy when reloading scene (need? or nah?)

        enemies = new List<Enemy>();

        boardScript = GetComponent<BoardManager>();

        timeLeft = timerDuration;

        InitGame();
	}

    private void startRound()
    {
        atkPuzSolved = 0;
        defPuzSolved = 0;
        puzzlePhase = true;
    }

    private void InitGame()
    {
        puzzlePhase = false;    // game has not begun yet

        timerText = GameObject.Find("Timer").GetComponent<Text>();
        puzzleSolvedText = GameObject.Find("PuzzleSolvedText").GetComponent<Text>();

        enemies.Clear(); // clear monster in list to prepare for next level

        boardScript.SetupScene(level);  // setup bg

        startRound();
    }

    //Call this to add the passed in Enemy to the List of Enemy objects.
    public void AddEnemyToList(Enemy script)
    {
        //Add Enemy to List enemies.
        enemies.Add(script);
    }

    private void OnLevelWasLoaded(int index)
    {
        
    }

    // when timer sends message to this controller that time has passed
    public void timeElapsed()
    {
        puzzlePhase = false; // end the puzzle phase
    }

    private void puzzleComplete (int atkScore, int defScore)
    {

    }

    static public void callBackButtonPress (string name)
    {

    }

    // Update is called once per frame
    void Update () {
        
		if (puzzlePhase)
        {
            // count down timer for puzzle phase
            timerText.text = "Timer : " + (int)timeLeft;
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timerText.text = "Attacking";
                puzzlePhase = false;
                timeLeft = timerDuration;
            }

            // For prototype: using keyboard input as "puzzle solved"
            if (Input.GetKeyDown("a"))
            {
                atkPuzSolved++;
            }
            if (Input.GetKeyDown("d"))
            {
                defPuzSolved++;
            }
        }

        puzzleSolvedText.text = "Atk Puzzles Solved: " + atkPuzSolved + "\n"
                                + "Def Puzzles Solved: " + defPuzSolved;
	}

    public static int getLevel()
    {
        return level;
    }
}


