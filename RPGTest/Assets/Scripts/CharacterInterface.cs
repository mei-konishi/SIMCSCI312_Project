﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterInterface : MonoBehaviour {

    protected int level;
    protected int strength;
    protected int defence;
    protected int maxHealth;
    protected int currentHealth;
    protected bool statusDead;
    protected float statusStun;

    // Use this for initialization
    protected virtual void Start () {
		
	}

    public virtual void updateStats ()
    {
        /*
        statsText.text = "Str: " + strength + " \n"
                        + "Def: " + defence + "\n"
                        + "Health: " + currentHealth + "/" + maxHealth;
                        */
    }

    public virtual void levelUp()
    {
        level++;
    }

    public void receiveDamage(int dmg)
    {
        if (dmg > currentHealth)
        {
            currentHealth = 0;
            statusDead = true;
        }
        else
        {
            currentHealth -= dmg;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
