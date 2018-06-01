using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialization : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // check if the following player prefs already exist
        // if not, create them with initialize values
        if (!PlayerPrefs.HasKey("level"))
            PlayerPrefs.SetInt("level", 1);

        if (!PlayerPrefs.HasKey("exp"))
            PlayerPrefs.SetInt("exp", 0);

        if (!PlayerPrefs.HasKey("str"))
            PlayerPrefs.SetInt("str", 99);

        if (!PlayerPrefs.HasKey("def"))
            PlayerPrefs.SetInt("def", 99);

        if (!PlayerPrefs.HasKey("hp"))
            PlayerPrefs.SetInt("hp", 900);

        if (!PlayerPrefs.HasKey("statsPoints"))
            PlayerPrefs.SetInt("statsPoints", 0);

        if (!PlayerPrefs.HasKey("skillPoints"))
            PlayerPrefs.SetInt("skillPoints", 0);

        if (!PlayerPrefs.HasKey("skillsLevelsUnlocked"))
            PlayerPrefs.SetString("skillsLevelsUnlocked", "44441");

        if (!PlayerPrefs.HasKey("skillsEquipped"))
            PlayerPrefs.SetString("skillsEquipped", "110");

        if (!PlayerPrefs.HasKey("equippedSkillsLevels"))
            PlayerPrefs.SetString("equippedSkillsLevels", "110");

        if (!PlayerPrefs.HasKey("stageUnlocked"))
            PlayerPrefs.SetInt("stageUnlocked", 10);
        PlayerPrefs.SetInt("stageUnlocked", 6);
        if (!PlayerPrefs.HasKey("stageLastPlayed"))
            PlayerPrefs.SetInt("stageLastPlayed", 0);

        if (!PlayerPrefs.HasKey("stageSelected"))
            PlayerPrefs.SetInt("stageSelected", 1);

        if (!PlayerPrefs.HasKey("tutorial"))
            PlayerPrefs.SetInt("tutorial", 0);

        if (!PlayerPrefs.HasKey("masterVolume"))
            PlayerPrefs.SetFloat("masterVolume", 0.7f);

        if (!PlayerPrefs.HasKey("sfxVolume"))
            PlayerPrefs.SetFloat("sfxVolume", 0.7f);

        if (!PlayerPrefs.HasKey("bgmVolume"))
            PlayerPrefs.SetFloat("bgmVolume", 0.7f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
