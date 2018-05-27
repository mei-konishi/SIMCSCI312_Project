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

    public static void StringToIntArray(string input, int[] output)
    {
        int size = input.Length;
        int placeHolder = int.Parse(input);
        if (size == 5)
        {
            output[0] = placeHolder / 10000; // extract first digit
            placeHolder %= 10000; // remove first digit
            output[1] = placeHolder / 1000; // extract second digit
            placeHolder %= 1000; // remove second digit
            output[2] = placeHolder / 100; // extract third digit
            placeHolder %= 100; // remove third digit
            output[3] = placeHolder / 10; // extract forth digit
            placeHolder %= 10; // remove forth digit
            output[4] = placeHolder; // extract last difit
        }
        else if (size == 3)
        {
            output[0] = placeHolder / 100; // extract first digit
            placeHolder %= 100; // remove first digit
            output[1] = placeHolder / 10; // extract second digit
            placeHolder %= 10; // remove second digit
            output[2] = placeHolder; // extract last digit
        }
        
    }

    public static string IntArrayToString(int[] input)
    {
        string output = "" + input[0].ToString() + input[1].ToString() + input[2].ToString();
        return output;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
