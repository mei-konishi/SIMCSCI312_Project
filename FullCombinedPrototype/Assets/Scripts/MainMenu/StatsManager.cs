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
    List<string> attackSkills = new List<string>();
    List<string> defendSkills = new List<string>();
    public Dropdown attackDropdown;
    public Dropdown defendDropdown;
    List<int> attackChoice = new List<int>();
    List<int> attackChoiceLevel = new List<int>();
    List<int> defendChoice = new List<int>();
    List<int> defendChoiceLevel = new List<int>();
    public Text attackLabel;
    public Text defendLabel;

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
        currentAtk1Lvl.text = ":" + extractSkillsUnlocked[0].ToString();
        currentAtk2Lvl.text = ":" + extractSkillsUnlocked[1].ToString();
        currentDef1Lvl.text = ":" + extractSkillsUnlocked[2].ToString();
        currentDef2Lvl.text = ":" + extractSkillsUnlocked[3].ToString();

        //Equipment
        equippedSkills = new int[3];
        equippedSkillsLevel = new int[3];
        Formulas.StringToIntArray(PlayerPrefs.GetString("skillsEquipped"), equippedSkills);
        Formulas.StringToIntArray(PlayerPrefs.GetString("equippedSkillsLevels"), equippedSkillsLevel);
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
        if (currentAvailSkills > 0 && extractSkillsUnlocked[0] < 4)
        {
            extractSkillsUnlocked[0]++;
            currentAtk1Lvl.text = ":" + extractSkillsUnlocked[0].ToString();
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
        if (currentAvailSkills > 0 && extractSkillsUnlocked[1] < 4)
        {
            extractSkillsUnlocked[1]++;
            currentAtk2Lvl.text = ":" + extractSkillsUnlocked[1].ToString();
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
        if (currentAvailSkills > 0 && extractSkillsUnlocked[2] < 4)
        {
            extractSkillsUnlocked[2]++;
            currentDef1Lvl.text = ":" + extractSkillsUnlocked[2].ToString();
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
        if (currentAvailSkills > 0 && extractSkillsUnlocked[3] < 4)
        {
            extractSkillsUnlocked[3]++;
            currentDef2Lvl.text = ":" + extractSkillsUnlocked[3].ToString();
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
        attackDropdown.ClearOptions();
        attackSkills.Clear();
        int currentAttack = 0;
        if (extractSkillsUnlocked[0] != 0)
        {
            for (int i = 1; i <= extractSkillsUnlocked[0]; i++)
            {
                attackSkills.Add("SimonSays: " + i);
                attackChoice.Add(1);
                attackChoiceLevel.Add(i);
                if (equippedSkills[0] == 1 && equippedSkillsLevel[0] == i)
                {
                    currentAttack = attackSkills.Count-1;
                }
            }       
        }
        if (extractSkillsUnlocked[1] != 0)
        {
            for (int i = 1; i <= extractSkillsUnlocked[1]; i++)
            {
                attackSkills.Add("FollowLeader: " + i);
                attackChoice.Add(2);
                attackChoiceLevel.Add(i);
                if (equippedSkills[0] == 2 && equippedSkillsLevel[0] == i)
                {
                    currentAttack = attackSkills.Count-1;
                }
            }
        }
        attackDropdown.AddOptions(attackSkills);
        attackDropdown.value = currentAttack;
    }

    public void PopulateDefendList()
    {
        //defendSkills
        defendDropdown.ClearOptions();
        defendSkills.Clear();
        int currentDefend = 0;
        if (extractSkillsUnlocked[2] != 0)
        {
            for (int i = 1; i <= extractSkillsUnlocked[2]; i++)
            {
                defendSkills.Add("MemoryPuzzle: " + i);
                defendChoice.Add(1);
                defendChoiceLevel.Add(i);
                if (equippedSkills[1] == 1 && equippedSkillsLevel[1] == i)
                {
                    currentDefend = defendSkills.Count-1;
                }
            }
        }
        if (extractSkillsUnlocked[3] != 0)
        {
            for (int i = 1; i <= extractSkillsUnlocked[3]; i++)
            {
                defendSkills.Add("MathPuzzle: " + i);
                defendChoice.Add(2);
                defendChoiceLevel.Add(i);
                if (equippedSkills[1] == 2 && equippedSkillsLevel[1] == i)
                {
                    currentDefend = defendSkills.Count-1;
                }
            }
        }
        defendDropdown.AddOptions(defendSkills);
        defendDropdown.value = currentDefend;
    }

    public void AttackInput(int input)
    {
        equippedSkills[0] = attackChoice[input];
        equippedSkillsLevel[0] = attackChoiceLevel[input];
        SaveEquip();
    }

    public void DefendInput(int input)
    {
        equippedSkills[1] = defendChoice[input];
        equippedSkillsLevel[1] = defendChoiceLevel[input];
        SaveEquip();
    }

    public void SaveEquip()
    {
        string newEquip = "";
        for (int i = 0; i < equippedSkills.Length; i++)
        {
            newEquip += equippedSkills[i].ToString();
        }
        PlayerPrefs.SetString("skillsEquipped", newEquip);

        string newEquipLevel = "";
        for (int i = 0; i < equippedSkillsLevel.Length; i++)
        {
            newEquipLevel += equippedSkillsLevel[i].ToString();
        }
        PlayerPrefs.SetString("equippedSkillsLevels", newEquipLevel);
    }

    private void initialSkillLoad()
    {
        string attackString = "";
        if (equippedSkills[0] == 1)
        {
            attackString += "SimonSays: " + equippedSkillsLevel[0];
        }
        else if (equippedSkills[0] == 2)
        {
            attackString += "FollowLeader: " + equippedSkillsLevel[0];
        }
        attackLabel.text = attackString;

        string defendString = "";
        if (equippedSkills[1] == 1)
        {
            defendString += "MemoryPuzzle: " + equippedSkillsLevel[1];
        }
        else if (equippedSkills[1] == 2)
        {
            defendString += "MathPuzzle: " + equippedSkillsLevel[1];
        }
        defendLabel.text = defendString;
    }
}
