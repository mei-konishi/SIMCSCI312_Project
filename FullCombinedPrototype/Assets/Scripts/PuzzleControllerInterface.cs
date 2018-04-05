using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleControllerInterface : MonoBehaviour {

    protected bool slotted; // when the puzzle is being selected in skill set
    protected bool active; // when puzzle is playable 

	// Use this for initialization
	public virtual void Start () {
        slotted = false;
        active = false;
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
