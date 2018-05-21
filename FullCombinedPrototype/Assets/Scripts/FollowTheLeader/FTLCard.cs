using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTLCard : MonoBehaviour {

    [SerializeField]
    private FollowTheLeaderController controller;
    private int id;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        controller.CheckCard(this);
        if(controller.CheckWin())
        {
            controller.SetupCards();
            controller.StartGame();
        }
    }

    public int getID
    {
        get { return id; }
    }

    public void setID(int newID)
    {
        id = newID;
    }


}
