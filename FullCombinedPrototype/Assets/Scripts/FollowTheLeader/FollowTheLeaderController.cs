using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheLeaderController : PuzzleControllerInterface
{
    // Override Variables
    public bool IsEnable = false;

    // Variables
    public List<FTLCard> cards;

    private bool[] gameStateCheck = new bool[9]; //default is false
    private List<int> cardList = new List<int>();
    private Queue<int> cardsToTap = new Queue<int>();   
    private int cardsLeftToLight = 0;


    // Override Functions
    public override void Start () {

        base.Start();
        puzzleType = 1; // this puzzle is a attack type
        puzzleDifficulty = puzzleManagerScript.CheckLevel(puzzleType);
        
    }

    public override void Play()
    {
        IsEnable = true;
        SetupCards();
        StartGame();
    }

    public override void Stop()
    {
        IsEnable = false;
    }

    // Update is called once per frame
    void Update () {

    }

    // Functions Here
    public void StartGame()
    {        
        // Add the cards to tap to the queue
        StartCoroutine(WaitForReveal());
    }

    private IEnumerator WaitForReveal()
    {
        int temp;
        int index;
        while (cardsLeftToLight > 0)
        {
            temp = Random.Range(0, cardList.Count);
            index = cardList[temp];
            cardList.RemoveAt(temp);
            gameStateCheck[index] = true;
            cardsToTap.Enqueue(index);

            // HOW TO MAKE CARD BECOME ACTIVE
            cards[index].gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1);

            cardsLeftToLight--;
        }      
    }

    public void CheckCard(FTLCard card)
    {
        if (card.getID == cardsToTap.Peek())
        {
            int cardID = card.getID;
            cardList.Add(cardID);
            gameStateCheck[cardID] = false;
            cardsToTap.Dequeue();
            cards[cardID].gameObject.SetActive(false);
        }
    }

    public bool CheckWin()
    {
        if (cardsToTap.Count == 0)
        {
            for (int i = 1; i <= gameStateCheck.Length; i++)
            {
                if (gameStateCheck[i-1] == true)
                {
                    return false;
                }
            }
            // Reset number of cards for next round if true
            puzzleManagerScript.PuzzleSolved(puzzleType);
            //SetupCards();
            //StartGame();
            return true;
        } else
        {
            return false;
        }       
    }

    public void SetupCards()
    {
        cardsLeftToLight = puzzleDifficulty + 2;
        cardsToTap.Clear();
        cardList.Clear();
        for (int i = 0; i < cards.Count; i++)
        {    
            gameStateCheck[i] = false;          
            cards[i].setID(i);
            cards[i].gameObject.SetActive(false);
            cardList.Add(i);
        }
    }

}
