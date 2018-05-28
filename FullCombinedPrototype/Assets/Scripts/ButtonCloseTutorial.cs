using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCloseTutorial : MonoBehaviour {

    public int thisButtonNumber;

    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}

    private void OnMouseDown()
    {
        gameManager.closeTutorial(thisButtonNumber);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
