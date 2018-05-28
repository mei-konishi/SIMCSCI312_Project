using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpRewardScript : MonoBehaviour {

    public Text reward;

	// Use this for initialization
	void Start () {

	}

    void Update ()
    {
        reward.text = PlayerPrefs.GetString("rewardString");
    }
}
