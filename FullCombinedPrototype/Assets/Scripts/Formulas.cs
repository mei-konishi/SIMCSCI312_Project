using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // do damage calculation here 
    public int calculateDmg(int atk, int def)
    {
        int dmg = atk - def;
        if (dmg < 0)
        {
            dmg = 0;
        }
        return dmg;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
