using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

    public Slider expBar;
    public Text currentLevel;
    public Text expToNext;
    private int currentLvl;
    private int expRequired;
    private int currentExp;

    public Text currentStr;
    public Text currentDef;
    public Text currentHP;
    public Text currentStats;
    private int currentAvailStats;

    public Text currentSkills;

    // Use this for initialization
    void Start () {

        currentLevel.text = PlayerPrefs.GetInt("level").ToString();
        currentLvl = PlayerPrefs.GetInt("level");
        expRequired = (int)(Mathf.Ceil(Mathf.Pow(currentLvl, 3.0F) * 0.03f + Mathf.Pow(currentLvl, 2.0f) * 0.1f + currentLvl * 50));
        currentExp = PlayerPrefs.GetInt("exp");
        expToNext.text = (expRequired - currentExp).ToString();
        expBar.value = currentExp / expRequired;

        //PlayerPrefs.SetInt("statsPoints", 10);
        currentStr.text = PlayerPrefs.GetInt("str").ToString();
        currentDef.text = PlayerPrefs.GetInt("def").ToString();
        currentHP.text = PlayerPrefs.GetInt("hp").ToString();
        currentStats.text = PlayerPrefs.GetInt("statsPoints").ToString();
        currentAvailStats = PlayerPrefs.GetInt("statsPoints");

        //PlayerPrefs.SetInt("skillPoints", 10);
        currentSkills.text = PlayerPrefs.GetInt("skillPoints").ToString();
    }
	
	// Update is called once per frame
	void Update () {
  
    }

    public void AddStr ()
    {
        if (currentAvailStats > 0)
        {
            int str = PlayerPrefs.GetInt("str");
            str++;
            PlayerPrefs.SetInt("str", str);
            currentStr.text = PlayerPrefs.GetInt("str").ToString();
            currentAvailStats--;
            PlayerPrefs.SetInt("statsPoints", currentAvailStats);
            currentStats.text = PlayerPrefs.GetInt("statsPoints").ToString();
        } else
        {
            // Play sound here
        }
        
    }

    public void AddDef()
    {
        if (currentAvailStats > 0)
        {
            int def = PlayerPrefs.GetInt("def");
            def++;
            PlayerPrefs.SetInt("def", def);
            currentDef.text = PlayerPrefs.GetInt("def").ToString();
            currentAvailStats--;
            PlayerPrefs.SetInt("statsPoints", currentAvailStats);
            currentStats.text = PlayerPrefs.GetInt("statsPoints").ToString();
        }
        else
        {
            // Play sound here
        }
    }

    public void AddHP()
    {
        if (currentAvailStats > 0)
        {
            int hp = PlayerPrefs.GetInt("hp");
            hp += 10;
            PlayerPrefs.SetInt("hp", hp);
            currentHP.text = PlayerPrefs.GetInt("hp").ToString();
            currentAvailStats--;
            PlayerPrefs.SetInt("statsPoints", currentAvailStats);
            currentStats.text = PlayerPrefs.GetInt("statsPoints").ToString();
        }
        else
        {
            // Play sound here
        }
    }

}
