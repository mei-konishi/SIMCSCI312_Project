using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : CharacterInterface {

    private int currentExp;
    private int expForNextLevel = 51; // for prototype's simplicity sake, using a single fixed value
                                 // for future, required to make either a database of exp for level, or formula

    public Text playerStatsText;
    private string rewardString;

    private Animator animator;  //Used to store a reference to the Player's animator component.

    private StatsUIManager statsUIManager;

    // Use this for initialization
    protected override void Start () {

        GameManager.instance.AddPlayerToManager(this);

        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();

        statsUIManager = FindObjectOfType<StatsUIManager>();

        initializeStats();
    }

    private void initializeStats()
    {
        level = PlayerPrefs.GetInt("level");
        strength = PlayerPrefs.GetInt("str");
        defence = PlayerPrefs.GetInt("def");
        maxHealth = PlayerPrefs.GetInt("hp");
        currentHealth = maxHealth;
        currentExp = PlayerPrefs.GetInt("exp");
        expForNextLevel = (int)Mathf.Ceil(Mathf.Pow(level, 3.0F) * 0.03f + Mathf.Pow(level, 2.0f) * 0.1f + level * 50);
        StatsUIManager.InitPlayerStats(strength, defence, maxHealth); // update the UI
    }

    public override void updateStats()
    {
        
    }

    public override void receiveDamage(int dmg)
    {
        base.receiveDamage(dmg);

        statsUIManager.UpdatePlayerHealth(currentHealth); // update UI
    }

    public void gainExp(int exp)
    {
        currentExp += exp;
        rewardString = "You have earned " + exp + " exp!";
        // if exp gained is enough to level up
        if (currentExp >= expForNextLevel)
        {
            // remove exp used for level up
            currentExp -= expForNextLevel; 
            levelUp();
        }
        PlayerPrefs.SetInt("exp", currentExp);
        PlayerPrefs.SetString("rewardString", rewardString);  
    }

    public override void levelUp()
    {
        base.levelUp();
        // TODO: in future, make a exp chart database, or some algorithm to generate next exp req
        // =ROUNDUP((A2^3)*0.03+(A2^2)*0.1+50*A2)
        expForNextLevel = (int)Mathf.Ceil( Mathf.Pow(level, 3.0F) * 0.03f + Mathf.Pow(level, 2.0f) * 0.1f + level * 50);
        PlayerPrefs.SetInt("level", level);
        int statsPoints = PlayerPrefs.GetInt("statsPoints") + 5;
        int skillPoints = PlayerPrefs.GetInt("skillPoints") + 2;
        PlayerPrefs.SetInt("statsPoints", statsPoints);
        PlayerPrefs.SetInt("skillPoints", skillPoints);
        rewardString += "You leveled up! 5 stats points and 2 skill points added.";
    }

    // Update is called once per frame
    void Update () {

        if (Input.touchCount > 0){ // check for touches
            Touch myTouch = Input.touches[0]; // get touch
        }
	}

    public void doHitAnimation()
    {
        animator.SetTrigger("PlayerAttack");
    }
}
