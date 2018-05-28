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
    private int currentAvailSkills;
    private int[] extractSkillsUnlocked;
    public Text currentAtk1Lvl;
    public Text currentAtk2Lvl;
    public Text currentDef1Lvl;
    public Text currentDef2Lvl;

    private int[] equippedSkills;
    private int[] equippedSkillsLevel;
    //public Text atkSkillText;
    //public Text atkSkillLevelText;
    //public Text defSkillText;
    //public Text defSkillLevelText;
    List<string> attackSkills = new List<string>();
    List<string> defendSkills = new List<string>();
    public Dropdown attackDropdown;
    public Dropdown defendDropdown;

    // Use this for initialization
    void Start () {

        //Exp Bar
        currentLevel.text = PlayerPrefs.GetInt("level").ToString();
        currentLvl = PlayerPrefs.GetInt("level");
        expRequired = (int)(Mathf.Ceil(Mathf.Pow(currentLvl, 3.0F) * 0.03f + Mathf.Pow(currentLvl, 2.0f) * 0.1f + currentLvl * 50));
        currentExp = PlayerPrefs.GetInt("exp");
        expToNext.text = (expRequired - currentExp).ToString();
        expBar.value = currentExp / expRequired;

        //Stats
        currentStr.text = PlayerPrefs.GetInt("str").ToString();
        currentDef.text = PlayerPrefs.GetInt("def").ToString();
        currentHP.text = PlayerPrefs.GetInt("hp").ToString();
        currentStats.text = PlayerPrefs.GetInt("statsPoints").ToString();
        currentAvailStats = PlayerPrefs.GetInt("statsPoints");

        //Skills - Atk1, Atk2, Def1, Def2, Ulti
        currentSkills.text = PlayerPrefs.GetInt("skillPoints").ToString();
        currentAvailSkills = PlayerPrefs.GetInt("skillPoints");
        extractSkillsUnlocked = new int[5];
        Formulas.StringToIntArray(PlayerPrefs.GetString("skillsLevelsUnlocked"), extractSkillsUnlocked);
        currentAtk1Lvl.text = extractSkillsUnlocked[0].ToString();
        currentAtk2Lvl.text = extractSkillsUnlocked[1].ToString();
        currentDef1Lvl.text = extractSkillsUnlocked[2].ToString();
        currentDef2Lvl.text = extractSkillsUnlocked[3].ToString();

        //Equipment
        //PlayerPrefs.SetString("skillsEquipped", "110");
        //PlayerPrefs.SetString("equippedSkillsLevels", "110");
        equippedSkills = new int[3];
        equippedSkillsLevel = new int[3];
        Formulas.StringToIntArray(PlayerPrefs.GetString("skillsEquipped"), equippedSkills);
        Formulas.StringToIntArray(PlayerPrefs.GetString("equippedSkillsLevels"), equippedSkillsLevel);
        /*if (equippedSkills[0] == 1)
        {
            atkSkillText.text = "Atk1";
        } else if (equippedSkills[0] == 2)
        {
            atkSkillText.text = "Atk2";
        }
        if (equippedSkills[1] == 1)
        {
            defSkillText.text = "Def1";
        }
        else if (equippedSkills[1] == 2)
        {
            defSkillText.text = "Def2";
        }
        atkSkillLevelText.text = equippedSkillsLevel[0].ToString();
        defSkillLevelText.text = equippedSkillsLevel[1].ToString();*/
        PopulateAttackList();
        PopulateDefendList();
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

    public void AddAtk1()
    {
        if (currentAvailSkills > 0 && extractSkillsUnlocked[0] < 5)
        {
            extractSkillsUnlocked[0]++;
            currentAtk1Lvl.text = extractSkillsUnlocked[0].ToString();
            string updatedSkills = "";
            for (int i = 0; i < extractSkillsUnlocked.Length; i++)
            {
                updatedSkills += extractSkillsUnlocked[i].ToString();
            }
            PlayerPrefs.SetString("skillsLevelsUnlocked", updatedSkills);
            currentAvailSkills--;
            PlayerPrefs.SetInt("skillPoints", currentAvailSkills);
            currentSkills.text = PlayerPrefs.GetInt("skillPoints").ToString();
        } else
        {
            // Play sound here
        }
    }

    public void AddAtk2()
    {
        if (currentAvailSkills > 0 && extractSkillsUnlocked[1] < 5)
        {
            extractSkillsUnlocked[1]++;
            currentAtk2Lvl.text = extractSkillsUnlocked[1].ToString();
            string updatedSkills = "";
            for (int i = 0; i < extractSkillsUnlocked.Length; i++)
            {
                updatedSkills += extractSkillsUnlocked[i].ToString();
            }
            PlayerPrefs.SetString("skillsLevelsUnlocked", updatedSkills);
            currentAvailSkills--;
            PlayerPrefs.SetInt("skillPoints", currentAvailSkills);
            currentSkills.text = PlayerPrefs.GetInt("skillPoints").ToString();
        }
        else
        {
            // Play sound here
        }
    }

    public void AddDef1()
    {
        if (currentAvailSkills > 0 && extractSkillsUnlocked[2] < 5)
        {
            extractSkillsUnlocked[2]++;
            currentDef1Lvl.text = extractSkillsUnlocked[2].ToString();
            string updatedSkills = "";
            for (int i = 0; i < extractSkillsUnlocked.Length; i++)
            {
                updatedSkills += extractSkillsUnlocked[i].ToString();
            }
            PlayerPrefs.SetString("skillsLevelsUnlocked", updatedSkills);
            currentAvailSkills--;
            PlayerPrefs.SetInt("skillPoints", currentAvailSkills);
            currentSkills.text = PlayerPrefs.GetInt("skillPoints").ToString();
        }
        else
        {
            // Play sound here
        }
    }

    public void AddDef2()
    {
        if (currentAvailSkills > 0 && extractSkillsUnlocked[3] < 5)
        {
            extractSkillsUnlocked[3]++;
            currentDef2Lvl.text = extractSkillsUnlocked[3].ToString();
            string updatedSkills = "";
            for (int i = 0; i < extractSkillsUnlocked.Length; i++)
            {
                updatedSkills += extractSkillsUnlocked[i].ToString();
            }
            PlayerPrefs.SetString("skillsLevelsUnlocked", updatedSkills);
            currentAvailSkills--;
            PlayerPrefs.SetInt("skillPoints", currentAvailSkills);
            currentSkills.text = PlayerPrefs.GetInt("skillPoints").ToString();
        }
        else
        {
            // Play sound here
        }
    }

    public void PopulateAttackList()
    {
        //attackSkills
        if (extractSkillsUnlocked[0] != 0)
        {
            for (int i = 1; i <= extractSkillsUnlocked[0]; i++)
            {
                attackSkills.Add("SimonSays: " + i);
                //atkchoice = 1
                //atklevelchoice = i;
            }       
        }
        if (extractSkillsUnlocked[1] != 0)
        {
            for (int i = 1; i <= extractSkillsUnlocked[1]; i++)
            {
                attackSkills.Add("FollowLeader: " + i);
            }
        }
        attackDropdown.AddOptions(attackSkills);
    }

    public void PopulateDefendList()
    {
        //defendSkills
        if (extractSkillsUnlocked[2] != 0)
        {
            for (int i = 1; i <= extractSkillsUnlocked[2]; i++)
            {
                defendSkills.Add("Concentration: " + i);

            }
        }
        if (extractSkillsUnlocked[3] != 0)
        {
            for (int i = 1; i <= extractSkillsUnlocked[3]; i++)
            {
                defendSkills.Add("Fortnite: " + i);
            }
        }
        defendDropdown.AddOptions(defendSkills);
    }

    public void AttackInput(int input)
    {
        //equippedSkills
        //equippedSkillsLevel
    }

    public void DefendInput(int input)
    {

    }
}
