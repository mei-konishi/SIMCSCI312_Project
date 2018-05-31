using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public static Timer instance = null; // Static instance of Timer
    public float puzzleDuration;

    private float timeLeft;
    private Text timerText;
    private float splashScreenDelay = 2.0f;
    private float animationDelay = 2.0f;
    private float warningAnimationDuration = 4.0f;
    private float bossAppearAnimationDuration = 4.0f;

    private GameManager gameManager;

    private static int phase;   // 1 for puzzle phase, 2 for attack phase, 0 for tutorial, 
                                // 3 for puzzle splash screen, 4 for attack splash screen
                                // 5 for warning, 6 for boss appear
    private static bool playerAtkTurn; // true for player's turn, false for enemy's turn
    private static bool animationTrigger; // true for animation to play (used as trigger switch)
    private static bool readyForNextRound; // true to start next round (used as a trigger switch)
    private static bool stop; // use for showing tutorial, or game over

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
        timeLeft = splashScreenDelay;

        timerText = GameObject.Find("Timer").GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();

        phase = 3;
        if (PlayerPrefs.GetInt("stageSelected") == 10) // if boss level
        {
            phase = 5; // set phase to warning 
            timeLeft = warningAnimationDuration;
        }
        playerAtkTurn = true;
        animationTrigger = true;
        readyForNextRound = false;
        stop = false;
    }

    public void resetTimer()
    {
        timeLeft = puzzleDuration;
        phase = 3;
    }

    public static void stopTimer()
    {
        stop = true;
    }

    public static bool checkPuzzlePhase()
    {
        if (phase == 1)
            return true;
        else
            return false;
    }

    public static bool checkPlayerTurn()
    {
        return playerAtkTurn;
    }

    // trigger function to check if timer says ready for next round
    public static bool checkReadyForNextRound()
    {
        // if timer says ready for next round, flip it back off and return true
        if (readyForNextRound)
        {
            readyForNextRound = false;
            return true;
        }

        else return false;
    }

    // function to check if need to trigger animation
    // also switches trigger back off if it's on
    // this way animation only plays once
    public static bool checkAnimationTriggered(string name)
    {
        // do separately for all animations...
        // i'm sorry I dunno how to make this neater ToT 
        if (name == "player" && playerAtkTurn)
        {
            // if animation trigger is on, flip it back off and return true to trigger
            if (animationTrigger)
            {
                animationTrigger = false;
                return true;
            }

            else return false;
        }

        if (name == "enemy" && !playerAtkTurn)
        {
            // if animation trigger is on, flip it back off and return true to trigger
            if (animationTrigger)
            {
                animationTrigger = false;
                return true;
            }

            else return false;
        }

        if (name == "splashPuzzle" && phase == 3)
        {
            if (animationTrigger)
            {
                animationTrigger = false;
                return true;
            }
            else return false;
        }

        if (name == "splashAttack" && phase == 4)
        {
            if (animationTrigger)
            {
                animationTrigger = false;
                return true;
            }
            else return false;
        }

        if (name == "bossAppearance" && phase == 6)
        {
            if (animationTrigger)
            {
                animationTrigger = false;
                return true;
            }
            else return false;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {

        // so long as timer isn't stopped
        if (!stop)
        {
            // count down timer for puzzle phase
            if (phase == 1)
            {
                timerText.text = "" + ((int)timeLeft + 1);
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0) // when time is up
                {
                    phase = 4;    // play attack phase splash screen
                    PuzzleManager.StopPuzzle(); // make the puzzles inactive
                    timeLeft = splashScreenDelay;
                    animationTrigger = true; // play splash screen animation
                }
            }

            // count down timer for animation delays ===================================

            if (phase == 3) // puzzle splash screen animation
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0) // when time is up
                {
                    // if player has never played the first game before
                    if (PlayerPrefs.GetInt("tutorial") == 0)
                    {
                        // stop timer and show tutorial screen
                        stop = true;
                        PlayerPrefs.SetInt("tutorial", 1); // set tutorial seen, go to next
                        gameManager.showTutorial(1);
                    }
                    else
                    {
                        startPuzzle();
                    }
                    // if player has never played the third game before (second attack) 
                    if (PlayerPrefs.GetInt("tutorial") == 2)
                    {
                        // stop timer and show tutorial screen
                        stop = true;
                        PlayerPrefs.SetInt("tutorial", 3); // set tutorial seen
                        gameManager.showTutorial(3);
                    }
                }
            }

            if (phase == 4) // attack splash screen animation
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0) // when time is up
                {
                    phase = 2; // go into attack mode
                    timeLeft = animationDelay;
                    animationTrigger = true; // play character attack animations
                }
            }

            if (phase == 2) // characters attacking animation
            {
                // count down for player's animation delay
                if (playerAtkTurn)
                {
                    timerText.text = "Player Attacking";
                    timeLeft -= Time.deltaTime;
                    if (timeLeft <= 0)
                    {
                        playerAtkTurn = false;  // player's turn ends
                        timeLeft = animationDelay;
                        animationTrigger = true; // play enemy's animation
                    }
                }

                // count down for enemy's animation delay
                if (!playerAtkTurn)
                {
                    timerText.text = "Enemy Attacking";
                    timeLeft -= Time.deltaTime;
                    if (timeLeft <= 0)  // enemy's turn ends
                    {
                        playerAtkTurn = true; // set back to player's turn for next round
                        phase = 3; // go to play puzzle splash screen 
                        timeLeft = splashScreenDelay;
                        animationTrigger = true;
                    }
                }
            }

            if (phase == 5) // boss warning animation
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    phase = 6; // go into boss appear mode
                    timeLeft = bossAppearAnimationDuration;
                    animationTrigger = true; // play boss appear animations
                }
            }

            if (phase == 6) // boss appearance animation
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    phase = 3; // go into boss appear mode
                    timeLeft = splashScreenDelay;
                    animationTrigger = true; // play boss appear animations
                }
            }
        }

        if (stop)
        {
          //  timerText.text = "";
        }
    }

    // function that does all the set up to prepare to start puzzle mode
    public void startPuzzle()
    {
        stop = false;
        phase = 1; // go into puzzle mode
        readyForNextRound = true; // tell timer that ready for puzzle round to start
        PuzzleManager.StartPuzzle(); // activate the puzzle again
        timeLeft = puzzleDuration; // set timer back to puzzle count down
        animationTrigger = true; // play character attack animations
    }
}
