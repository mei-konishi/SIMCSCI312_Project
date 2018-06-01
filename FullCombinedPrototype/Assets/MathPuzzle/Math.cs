using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Math : PuzzleControllerInterface {

    public GameObject[] myImage;
    public GameObject[] calculate;
    public GameObject canvas;
    private GameObject FN;
    private GameObject SN;
    private GameObject TN;
    private GameObject FoN;
    private GameObject CN;
    private GameObject CN2;
    private GameObject CN3;

    public int difficulty = 2;

    int result = 0;
    int points = 0;
    
    int d;
    int e;
    int f;
    int g;
    public Button myButton;

    // Use this for initialization
   public override void Start() {
        base.Start();

        puzzleType = 2; // this puzzle is a defence type
        puzzleDifficulty = 1;

        canvas.SetActive(false);

    //    createMath();  
    }

    void createMath()
    {
        GameObject.Destroy(FN);
        GameObject.Destroy(SN);
        GameObject.Destroy(TN);
        GameObject.Destroy(FoN);
        GameObject.Destroy(CN);
        GameObject.Destroy(CN2);
        GameObject.Destroy(CN3);

 

        Debug.Log(points);
        int firstnumber = Random.Range(1, 13);
        int secondnumber = Random.Range(1, 13);
        int thirdnumber = Random.Range(1, 13);
        int fourthnumber = Random.Range(1, 13);
        int firstoperator = Random.Range(0, 2);
        int secondoperator = Random.Range(0, 2);
        int thirdoperator = Random.Range(0, 2);

        GameObject firstNum = myImage[firstnumber];
        //FN = Instantiate(firstNum, new Vector3(-2, 0, 0f), Quaternion.identity) as GameObject;
        FN = Instantiate(firstNum, new Vector3(1.5f, -0.2f, 0f), Quaternion.identity) as GameObject;
     

        GameObject secondNum = myImage[secondnumber];
        //SN = Instantiate(secondNum, new Vector3(0.5f, 0, 0f), Quaternion.identity) as GameObject;
        SN = Instantiate(secondNum, new Vector3(3.8f, -0.2f, 0f), Quaternion.identity) as GameObject;

        GameObject thirdNum = myImage[thirdnumber];

        GameObject fourthNum = myImage[fourthnumber];

        GameObject calculator = calculate[firstoperator];
        //CN = Instantiate(calculator, new Vector3(-0.75f, 0, 0f), Quaternion.identity) as GameObject;
        CN = Instantiate(calculator, new Vector3(2.65f, -0.2f, 0f), Quaternion.identity) as GameObject;

        GameObject calculator2 = calculate[secondoperator];

        GameObject calculator3 = calculate[thirdoperator];

        switch (difficulty)
        {
            case 1:
                if (firstoperator == 0)
                {
                    result = firstnumber + secondnumber;
                }
                else
                    result = firstnumber - secondnumber;


                myButton.gameObject.SetActive(false);

                break;

            case 2:
                //CN2 = Instantiate(calculator2, new Vector3(1.75f, 0, 0f), Quaternion.identity) as GameObject;
                //TN = Instantiate(thirdNum, new Vector3(-2f, -1.5f, 0f), Quaternion.identity) as GameObject;
                CN2 = Instantiate(calculator2, new Vector3(5f, -0.2f, 0f), Quaternion.identity) as GameObject;
                TN = Instantiate(thirdNum, new Vector3(1.5f, -1.8f, 0f), Quaternion.identity) as GameObject;

                if (firstoperator == 0)
                {
                    result = firstnumber + secondnumber;
                }
                else
                    result = firstnumber - secondnumber;

                if (secondoperator == 0)
                {
                    result = result + thirdnumber;
                }
                else
                    result = result - thirdnumber;

                myButton.gameObject.SetActive(false);

                break;

            case 3:
                //CN2 = Instantiate(calculator2, new Vector3(1.75f, 0, 0f), Quaternion.identity) as GameObject;
                //TN = Instantiate(thirdNum, new Vector3(-2f, -1.5f, 0f), Quaternion.identity) as GameObject;
                CN2 = Instantiate(calculator2, new Vector3(5f, -0.2f, 0f), Quaternion.identity) as GameObject;
                TN = Instantiate(thirdNum, new Vector3(1.5f, -1.8f, 0f), Quaternion.identity) as GameObject;

                if (firstoperator == 0)
                {
                    result = firstnumber + secondnumber;
                }
                else
                    result = firstnumber - secondnumber;

                if (secondoperator == 0)
                {
                    result = result + thirdnumber;
                }
                else
                    result = result - thirdnumber;

                myButton.gameObject.SetActive(true);

                break;

            case 4:
                //CN2 = Instantiate(calculator2, new Vector3(1.75f, 0, 0f), Quaternion.identity) as GameObject;
                //TN = Instantiate(thirdNum, new Vector3(-2f, -1.5f, 0f), Quaternion.identity) as GameObject;
                //CN3 = Instantiate(calculator3, new Vector3(-0.75f, -1.5f, 0f), Quaternion.identity) as GameObject;
                //FoN = Instantiate(fourthNum, new Vector3(0.5f, -1.5f, 0f), Quaternion.identity) as GameObject;
                CN2 = Instantiate(calculator2, new Vector3(5f, -0.2f, 0f), Quaternion.identity) as GameObject;
                TN = Instantiate(thirdNum, new Vector3(1.5f, -1.8f, 0f), Quaternion.identity) as GameObject;
                CN3 = Instantiate(calculator3, new Vector3(2.65f, -1.8f, 0f), Quaternion.identity) as GameObject;
                FoN = Instantiate(fourthNum, new Vector3(3.8f, -1.8f, 0f), Quaternion.identity) as GameObject;

                if (firstoperator == 0)
                {
                    result = firstnumber + secondnumber;
                }
                else
                    result = firstnumber - secondnumber;

                if (secondoperator == 0)
                {
                    result = result + thirdnumber;
                }
                else
                    result = result - thirdnumber;

                if (thirdoperator == 0)
                {
                    result = result + fourthnumber;
                }
                else
                    result = result - fourthnumber;

                myButton.gameObject.SetActive(true);

                break;

        }
               

        int choose = Random.Range(0, 4);
        

        int x = Random.Range(-36, 49);
        x = checker(x, result);
        int y = Random.Range(-36, 49);
        y = checker(y, result);
        int z = Random.Range(-36, 49);
        z = checker(z, result);

        switch (choose)
        {
            case 0:
                GameObject.Find("ButtonA").GetComponentInChildren<Text>().text = result.ToString();
                d = result;
                GameObject.Find("ButtonB").GetComponentInChildren<Text>().text = x.ToString();
                e = x;
                GameObject.Find("ButtonC").GetComponentInChildren<Text>().text = y.ToString();
                f = y;

                if (difficulty >= 3)
                {
                    GameObject.Find("ButtonD").GetComponentInChildren<Text>().text = z.ToString();
                    g = z;
                }
               
                break;

            case 1:
                GameObject.Find("ButtonA").GetComponentInChildren<Text>().text = x.ToString();
                d = x;
                GameObject.Find("ButtonB").GetComponentInChildren<Text>().text = result.ToString();
                e = result;
                GameObject.Find("ButtonC").GetComponentInChildren<Text>().text = y.ToString();
                f = y;

                if (difficulty >= 3)
                {
                    GameObject.Find("ButtonD").GetComponentInChildren<Text>().text = z.ToString();
                    g = z;
                }
                break;

            case 2:
                GameObject.Find("ButtonA").GetComponentInChildren<Text>().text = x.ToString();
                d = x;
                GameObject.Find("ButtonB").GetComponentInChildren<Text>().text = y.ToString();
                e = y;
                GameObject.Find("ButtonC").GetComponentInChildren<Text>().text = result.ToString();
                f = result;
                if (difficulty >= 3)
                {
                    GameObject.Find("ButtonD").GetComponentInChildren<Text>().text = z.ToString();
                    g = z;
                }
                break;

            case 3:
                GameObject.Find("ButtonA").GetComponentInChildren<Text>().text = x.ToString();
                d = x;
                GameObject.Find("ButtonB").GetComponentInChildren<Text>().text = y.ToString();
                e = y;
                GameObject.Find("ButtonC").GetComponentInChildren<Text>().text = z.ToString();
                f = z;

                if (difficulty < 3)
                {
                    int t = Random.Range(0, 3);
                    if (t == 0)
                    {
                        GameObject.Find("ButtonA").GetComponentInChildren<Text>().text = result.ToString();
                        d = result;
                    }

                    else if (t == 1)
                    {
                        GameObject.Find("ButtonB").GetComponentInChildren<Text>().text = result.ToString();
                        e = result;
                    }

                    else
                    {
                        GameObject.Find("ButtonC").GetComponentInChildren<Text>().text = result.ToString();
                        f = result;
                    }
                }

                else
                {
                    GameObject.Find("ButtonD").GetComponentInChildren<Text>().text = result.ToString();
                    g = result;
                }

                break;
        }

       
    }

    public void onClickA()
    {
      //  Debug.Log(d);
        if (result != d)
        {
            createMath();
        }
        else
        {
            puzzleManagerScript.PuzzleSolved(puzzleType);
            points++;
            createMath();
        }
    }

    public void onClickB()
    {
      //  Debug.Log(e);
        if (result != e)
        {
            createMath();
        }
        else
        {
            puzzleManagerScript.PuzzleSolved(puzzleType);
            points++;
            createMath();
        }
    }

    public void onClickC()
    {
     //   Debug.Log(f);
        if (result != f)
        {
            createMath();
        }
        else
        {
            puzzleManagerScript.PuzzleSolved(puzzleType);
            points++;
            createMath();
        }
    }

    public void onClickD()
    {
     //   Debug.Log(g);
        if (result != g)
        {
            createMath();
        }
        else
        {
            puzzleManagerScript.PuzzleSolved(puzzleType);
            points++;
            createMath();
        }
    }

    int checker(int check, int result)
    {
        while (check == result)
        {
            check = Random.Range(-36, 49);
        }

        return check;
    }

    // Update is called once per frame
    void Update () {
 

    }

    public override void Play()
    {
        base.Play();
        canvas.SetActive(true);      
        createMath();
        FN.GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void Stop()
    {
        base.Stop();
        canvas.SetActive(false);        
  //      createMath();
        FN.GetComponent<SpriteRenderer>().enabled = false;
        SN.GetComponent<SpriteRenderer>().enabled = false;
        TN.GetComponent<SpriteRenderer>().enabled = false;
        FoN.GetComponent<SpriteRenderer>().enabled = false;
        CN.GetComponent<SpriteRenderer>().enabled = false;
        CN2.GetComponent<SpriteRenderer>().enabled = false;
        CN3.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Restart()
    {
        createMath();
    }

}
