using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour {

    private bool attackPressed;
    private bool defendPressed;

	// Use this for initialization
	void Start () {
        attackPressed = false;
        defendPressed = false;
	}

    void OnTouchDown()
    {
        GameManager.callBackButtonPress(name);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
