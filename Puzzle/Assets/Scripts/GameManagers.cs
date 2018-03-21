using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagers : MonoBehaviour
{
    public int difficulty;

    public GameObject[] objects;
    public SpriteRenderer[] colours;
    public AudioSource[] buttonSounds;

    private int colourSelect;

    public float stayLit;
    private float stayLitCounter;

    public float waitBetweenLight;
    private float waitBetweenCounter;

    private bool shouldBeLit;
    private bool shouldBeDark;

    public List<int> activeSequence;
    private int positionInSequence;

    private bool gameActive;
    private int inputInSequence;

    public AudioSource correct;
    public AudioSource incorrect;

    public Text scoreText;

    private int points;

    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("HiScore"))
        {
            PlayerPrefs.SetInt("HiScore", 0);
        }

        scoreText.text = "Score: 0 - High Score: " + PlayerPrefs.GetInt("HiScore");
    }


    // Update is called once per frame
    void Update()
    {
        if (shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;

            if (stayLitCounter < 0)
            {
                colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.5f);
                buttonSounds[activeSequence[positionInSequence]].Stop();
                shouldBeLit = false;

                shouldBeDark = true;
                waitBetweenCounter = waitBetweenLight;

                positionInSequence++;
            }
        }

        if (shouldBeDark)
        {
            waitBetweenCounter -= Time.deltaTime;

            if (positionInSequence >= activeSequence.Count)
            {
                shouldBeDark = false;
                gameActive = true;
            }

            else
            {
                if (waitBetweenCounter < 0)
                {

                    colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);

                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }
    }

    public void StartGame()
    {
        activeSequence.Clear();

        positionInSequence = 0;
        inputInSequence = 0;

        for(int i = 0; i < difficulty +2; i++)
        {
            colourSelect = Random.Range(0, colours.Length);

            activeSequence.Add(colourSelect);

            colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
            buttonSounds[activeSequence[positionInSequence]].Play();

            stayLitCounter = stayLit;
            shouldBeLit = true;
        }

        scoreText.text = "Score: 0 - High Score: " + PlayerPrefs.GetInt("HiScore");
    }

    public void ColourPressed(int whichButton)
    {
        if (gameActive)
        {
                if (activeSequence[inputInSequence] == whichButton)
                {
                    Debug.Log("Correct");

                    inputInSequence++;

                    if (inputInSequence >= activeSequence.Count)
                    {
                        points++;
                        //if (activeSequence.Count > PlayerPrefs.GetInt("HiScore"))
                        if (points > PlayerPrefs.GetInt("HiScore"))
                        {
                            PlayerPrefs.SetInt("HiScore", points);
                        }

                        //scoreText.text = "Score: " + activeSequence.Count + " - HighScore: " + PlayerPrefs.GetInt("HiScore");
                        scoreText.text = "Score: " + points + " - HighScore: " + PlayerPrefs.GetInt("HiScore");

                        positionInSequence = 0;
                        inputInSequence = 0;
                        
                        activeSequence.Clear();
                        
                        for (int i = 0; i < difficulty + 2; i++)
                        {
                            colourSelect = Random.Range(0, colours.Length);

                            activeSequence.Add(colourSelect);

                            colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);

                            stayLitCounter = stayLit;
                            shouldBeLit = true;
                        }

                        gameActive = false;

                        correct.Play();
                    }
                }

                else
                {
                    Debug.Log("Wrong");
                    incorrect.Play();
                    gameActive = false;
                }
            
            
        }

    }
}