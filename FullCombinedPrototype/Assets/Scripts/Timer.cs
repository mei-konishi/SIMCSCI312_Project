using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static Timer instance = null; // Static instance of Timer
    public float puzzleDuration = 5.0f;

    private float timeLeft; 
    private Text timerText;
    private float animationDelay = 2.0f;

    private static bool puzzlePhase;   // true for puzzle phase, false for attack phase
    private static bool playerAtkTurn; // true for player's turn, false for enemy's turn
    private static bool animationTrigger; // true for animation to play (used as trigger switch)
    private static bool readyForNextRound; // true to start next round (used as a trigger switch)
    private static bool stop; // use for game over

	// Use this for initialization
	void Awake () {

        if (instance == null){ // check if instance already exists
            instance = this;    // if not, set instance to this
        }

        else if (instance != this){ // if instance already exists and is not this
            Destroy(gameObject);    // then destroy it. enforcing singleton
        }

        DontDestroyOnLoad(gameObject); // don't destroy when reloading scene (need? or nah?)

        timeLeft = puzzleDuration;

        timerText = GameObject.Find("Timer").GetComponent<Text>();

        puzzlePhase = true;   
        playerAtkTurn = true;
        animationTrigger = false;
        readyForNextRound = false;
        stop = false;
    }

    public void resetTimer()
    {
        timeLeft = puzzleDuration;
        puzzlePhase = true;
    }

    public static void stopTimer()
    {
        stop = true;
    }

    public static bool checkPuzzlePhase()
    {
        return puzzlePhase;
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
        // do separately for player and enemy...
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

        return false;
    }

    // Update is called once per frame
    void Update() {

        // so long as timer isn't stopped
        if (!stop)
        {
            // count down timer for puzzle phase
            if (puzzlePhase)
            {
                timerText.text = "Timer : " + ((int)timeLeft + 1);
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    puzzlePhase = false;    // enter attack phase
                    playerAtkTurn = true;   // player's turn first
                    timeLeft = animationDelay;
                    animationTrigger = true; // play player's animation
                }
            }

            // count down timer for animation delay
            if (!puzzlePhase)
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
                        readyForNextRound = true; // tell timer that ready for next round to start
                        puzzlePhase = true; // go back to puzzle mode
                        playerAtkTurn = true; // set back to player's turn for next round
                        timeLeft = puzzleDuration; // set timer back to puzzle count down
                    }
                }
            }
        }

        if (stop)
        {
            timerText.text = "GAME OVER";
        }
    }
}
