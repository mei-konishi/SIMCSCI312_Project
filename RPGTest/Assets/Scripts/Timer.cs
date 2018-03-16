using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float puzzleDuration = 5.0f;

    private float timeLeft;
    private Text timerText;
    private const float animationDelay = 2f;

    private bool puzzlePhase;

	// Use this for initialization
	void Awake () {
        timeLeft = puzzleDuration;

        timerText = GameObject.Find("Timer").GetComponent<Text>();

        puzzlePhase = true;
    }

    public void resetTimer()
    {
        timeLeft = puzzleDuration;
        puzzlePhase = true;
    }

    public bool checkPuzzlePhase()
    {
        return puzzlePhase;
    }
	
	// Update is called once per frame
	void Update () {

        if (puzzlePhase)
        {
            // count down timer for puzzle phase
            timerText.text = "Timer : " + ((int)timeLeft + 1);
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timerText.text = "Attacking";
                puzzlePhase = false;
                timeLeft = puzzleDuration;
            }
        }

        
		
    }
}
