using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharacterInterface
{
    private Animator animator;
    private PuzzleManager puzzleManager;
    private StatsUIManager statsUIManager;

    private float solvingSpeed; // the speed at which the enemy attempts to solve a puzzle
    private int solvingRate; // the chance of solving the puzzle
    private int puzzlePreferrence; // chance of solving attack puzzle over defence 

    private bool isTurn; // when the enemy can attempt to solve puzzle
    private bool moved = false; // a trigger to flip for starting coroutine 

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        SetLevel(GameManager.getLevel());

        //Register this enemy with our instance of GameManager by adding it to a list of Enemy objects. 
        //This allows the GameManager to issue movement commands.
        GameManager.instance.AddEnemyToList(this);

        //Get and store a reference to the attached Animator component.
        animator = GetComponent<Animator>();

        puzzleManager = FindObjectOfType<PuzzleManager>();
        statsUIManager = FindObjectOfType<StatsUIManager>();
    }

    public override void UpdateStats()
    {
        base.UpdateStats();
    }

    public override void ReceiveDamage(int dmg)
    {
        base.ReceiveDamage(dmg);

        statsUIManager.UpdateEnemyHealth(currentHealth); // update UI
    }

    public void SetLevel(int lvl)
    {
        level = lvl;
        // this is just for prototype. 
        // TODO : in future, make stats database, or some formula
        // =ROUNDDOWN(lvl^3*0.09 + lvl^2*0.08 + 1)
        strength = (int)Mathf.Floor(Mathf.Pow(lvl, 3) * 0.09f + Mathf.Pow(lvl, 2) * 0.08f + 1f);
        // =ROUNDDOWN(lvl^3*0.08 + lvl^2*0.07 + 1)
        defence = (int)Mathf.Floor(Mathf.Pow(lvl, 3) * 0.08f + Mathf.Pow(lvl, 2) * 0.07f + 1f);
        // =ROUNDUP(lvl^4*0.09 + 50)
        maxHealth = (int)Mathf.Ceil(Mathf.Pow(lvl, 4) * 0.09f + 18f);
        currentHealth = maxHealth;
        StatsUIManager.InitEnemyValues(strength, defence, maxHealth); // update UI 

        // setting hard values for now. will need to come up with formula or something later
        solvingSpeed = 8.0f; // attemps to solve at every 8 second
        solvingRate = 100; // has a 100% chance to solve the puzzle at every attempt.
        puzzlePreferrence = 90; // has a 90% chance to solve attack puzzle and 10% for defence
    }

    // Update is called once per frame
    void Update () {

        if (!isTurn) // when not turn 
        {
            StopCoroutine(SolvePuzzle()); // stop solving puzzles
        }
        else if (!moved) // when is turn, but haven't moved
        {
            moved = true; // set this to true to stop infinite move
            StartCoroutine(SolvePuzzle()); // start solving puzzles
        }
    }

    IEnumerator SolvePuzzle()
    {
        // take time to solve puzzle
        yield return new WaitForSeconds(solvingSpeed);
        // check if puzzle is solved 
        bool puzzleSolved = Random.Range(0, 100) <= solvingRate ? true : false;
        // check if attack puzzle solved or defence
        bool attackPuzzle = Random.Range(0, 100) <= puzzlePreferrence ? true : false;
        // if puzzle is solved, update the puzzle manager
        if (puzzleSolved)
        {
            if (attackPuzzle)
            {
                puzzleManager.EnemyAtkPuzzleSolved();
            }
            else
            {
                puzzleManager.EnemyDefPuzzleSolved();
            }
        }
        moved = false; // set this back to false again to start next move
    }

    public void SetIsTurn(bool turn)
    {
        isTurn = turn;
    }

    public void DoHitAnimation()
    {
        animator.SetTrigger("EnemyAttack");
    }

    public void DoDmgedAnimation()
    {
        animator.SetTrigger("EnemyDamage");
    }

    public void BossAppearance()
    {
        animator.SetTrigger("BossAppear");
    }
}
