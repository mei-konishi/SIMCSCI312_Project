﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryPuzzleSceneController : MonoBehaviour {

    private int gridRows;
    private int gridCols;
    private int arrayAmt;
    public const float offsetX = 1.8f;
    public const float offsetY = 1.4f;

    private GameObject DiffSlider;
    public int diffLv;
    private int ab = 0;
    int[] numbers;
    private MainCard card;
    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private int clearP = 0;

    // Use this for initialization
    void Start() {
      
        Vector3 startPos = originalCard.transform.position;

        SetUpCards(diffLv); // set number of rows and cols of cards based on level

        PlaceCardsInRandSlots(startPos);  // put cards in slots randomly        
    }

    private void SetUpCards(int level)
    {
        // set up cards based on level 
        gridRows = diffLv;
        gridCols = 4;
        arrayAmt = gridRows * gridCols;
    }

    private void PlaceCardsInRandSlots(Vector3 startPos)
    {
        int[] numbers = new int[arrayAmt];

        for (int a = 0; a < arrayAmt / 2; a++)
        {
            numbers[ab] = a;
            numbers[ab + 1] = a;
            ab += 2;
        }
        numbers = ShuffleArray(numbers);

        // Placing various cards in random slots
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                //    MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    void Update()
    {
        if(_score == arrayAmt/2) // win
        {
            PuzzleManager.AtkPuzzleSolved();
            PuzzleManager.DefPuzzleSolved();
            _score = 0;
            ab = 0;
            clearP++;
            Restart();
        }
    } 

    //-------------------------------------------------------------------------------------------------------------------------------------------

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;
    [SerializeField] private TextMesh scoreLabel;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            scoreLabel.text = "Score: " + _score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

    
    public void Restart()
    {
        GameObject[] tin = GameObject.FindGameObjectsWithTag("TheCard");
        foreach (GameObject tal in tin)
        {
            if (tal.name != "MainCard")
            {
                Destroy(tal);
            }
            tal.transform.GetChild(0).gameObject.SetActive(true);
        }

        SetUpCards(diffLv); // set number of rows and cols of cards based on level

        Vector3 startPos = originalCard.transform.position;

        PlaceCardsInRandSlots(startPos);  // put cards in slots randomly 
    }



}