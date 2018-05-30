using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        GameManager.instance.AddSplashScreenToManager(this);

        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SplashAttackScreen()
    {
        animator.SetTrigger("SplashAttack");
    }

    public void SplashPuzzleScreen()
    {
        animator.SetTrigger("SplashPuzzle");
    }
}
