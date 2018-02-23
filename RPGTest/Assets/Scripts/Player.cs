using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterInterface {

    private int currentExp;
    private int expForNextLevel; // for prototype's simplicity sake, using a single fixed value
                                 // for future, required to make either a database of exp for level, or formula

    private Text playerStatsText;

    // Use this for initialization
    protected override void Start () {

        initializeStats();
        
        playerStatsText = GameObject.Find("playerStatsText").GetComponent<Text>();

        playerStatsText.text = "Str: " + strength + " \n"
                             + "Def: " + defence + "\n"
                             + "Health: " + currentHealth + "/" + maxHealth;
    }

    private void initializeStats()
    {
        level = 1;
        strength = 1;
        defence = 1;
        maxHealth = 50;
        currentHealth = maxHealth;
        currentExp = 0;
    }

    public override void updateStats()
    {
        base.updateStats();
    }

    public void gainExp(int exp)
    {
        currentExp += exp;

        // if exp gained is enough to level up
        if (currentExp >= expForNextLevel)
        {
            // remove exp used for level up
            currentExp -= expForNextLevel; 
            levelUp();
        }
    }

    public override void levelUp()
    {
        base.levelUp();
        // TODO: in future, make a exp chart database, or some algorithm to generate next exp req
    }

    // Update is called once per frame
    void Update () {
        
		if (Input.touchCount > 0){ // check for touches
            Touch myTouch = Input.touches[0]; // get touch
        }
	}
}
