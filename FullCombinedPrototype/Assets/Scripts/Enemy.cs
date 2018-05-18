using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharacterInterface
{

    private Animator animator;

    public Text enemyStatsText;

    // Use this for initialization
    protected override void Start()
    {
        setLevel(GameManager.getLevel());

        //Register this enemy with our instance of GameManager by adding it to a list of Enemy objects. 
        //This allows the GameManager to issue movement commands.
        GameManager.instance.AddEnemyToList(this);

        //Get and store a reference to the attached Animator component.
        animator = GetComponent<Animator>();

 //       enemyStatsText = GameObject.Find("EnemyStatsText").GetComponent<Text>();

        base.Start();
        /*
        statsText.text = "Str: " + strength + " \n"
                             + "Def: " + defence + "\n"
                             + "Health: " + currentHealth + "/" + maxHealth;
                             */
    }

    public override void updateStats()
    {
        base.updateStats();
    }

    public void setLevel(int lvl)
    {
        // this is just for prototype. 
        // TODO : in future, make stats database, or some formula
        strength = lvl;
        defence = lvl - 1;
        maxHealth = lvl * 20;
        currentHealth = maxHealth;
        StatsUIManager.InitEnemyValues(strength, defence, maxHealth); // update UI 
    }

    // Update is called once per frame
    void Update () {
        /*
        enemyStatsText.text = "Str: " + strength + " \n"
                             + "Def: " + defence + "\n"
                             + "Health: " + currentHealth + "/" + maxHealth;
                             */
        StatsUIManager.UpdateEnemyHealth(currentHealth); // update UI
    }

    public void doHitAnimation()
    {
        animator.SetTrigger("EnemyAttack");
    }
}
