using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleControllerInterface : MonoBehaviour {

    protected bool slotted; // when the puzzle is being selected in skill set
    protected bool active; // when puzzle is playable 
    protected int puzzleType; // 1 for atk, 2 for def, 3 for ulti
    protected int puzzleDifficulty; // 1 to 5 only 

	// Use this for initialization
	public virtual void Start () {
        slotted = false;
        active = false;
        puzzleType = 0;
        puzzleDifficulty = 0;
	}
	
    // use this to start the puzzle
    public virtual void Play()
    {
        active = true;
    }

    // use this to stop the puzzle
    public virtual void Stop()
    {
        active = false;
    }


}
