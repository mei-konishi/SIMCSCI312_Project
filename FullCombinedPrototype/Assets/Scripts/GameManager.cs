using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.
    private BoardManager boardScript;           // script that creates bg 
    private PuzzleManager pmScript;             // script that handles puzzle switching and score keeping
    private Player player;                      // hold referrence to player script
    private List<Enemy> enemies;                // hold referrence to enemies script
    
    private Timer timer;

    public static int level = 1;      // used to keep track of stage 
    private int stageWin; // -1 for lose, 0 for ongoing, 1 for win

    // temp counter for number of puzzles solved
    private int playerAtkPuzSolved;
    private int playerDefPuzSolved;
    private int enemyAtkPuzSolved;
    private int enemyDefPuzSolved;

    private Text puzzleSolvedText;
    private Text enemyDmgedText;
    private Text playerDmgedText;
    private Text winText;

    // Use this for initialization
    void Awake () {

		if (instance == null){ // check if instance already exists
            instance = this;    // if not, set instance to this
        }

        else if (instance != this){ // if instance already exists and is not this
            Destroy(gameObject);    // then destroy it. enforcing singleton
        }

        DontDestroyOnLoad(gameObject); // don't destroy when reloading scene (need? or nah?)

        enemies = new List<Enemy>();    // allocate memory for enemies 

        boardScript = GetComponent<BoardManager>();
        pmScript = GetComponent<PuzzleManager>();
        timer = GetComponent<Timer>();

        InitGame();
	}

    // set or reset stats to prepare for next round
    private void startNextRound()
    {
        playerAtkPuzSolved = 0;
        playerDefPuzSolved = 0;
        enemyAtkPuzSolved = 0;
        enemyDefPuzSolved = 0;
    }

    private void InitGame()
    {
        stageWin = 0; 
        
        puzzleSolvedText = GameObject.Find("PuzzleSolvedText").GetComponent<Text>();
        enemyDmgedText = GameObject.Find("EnemyDamagedText").GetComponent<Text>();
        playerDmgedText = GameObject.Find("PlayerDamagedText").GetComponent<Text>();
        winText = GameObject.Find("YouWinText").GetComponent<Text>();

        enemies.Clear(); // clear monster in list to prepare for next level

        boardScript.SetupScene(level);  // setup bg

        startNextRound();
    }

    //Call this to add the passed in Enemy to the List of Enemy objects.
    public void AddEnemyToList(Enemy script)
    {
        //Add Enemy to List enemies.
        enemies.Add(script);
    }

    public void AddPlayerToManager(Player script)
    {
        player = script;
    }

    private void OnLevelWasLoaded(int index)
    {
        
    }
    

    // Update is called once per frame
    void Update () {
        
        // during puzzle phase
		if (timer.checkPuzzlePhase())
        {
            // For prototype: using keyboard input as "puzzle solved"
            if (Input.GetKeyDown("a"))
            {
                playerAtkPuzSolved++;
            }
            if (Input.GetKeyDown("d"))
            {
                playerDefPuzSolved++;
            }
        }

        puzzleSolvedText.text = "Atk Puzzles Solved: " + playerAtkPuzSolved + "\n"
                                + "Def Puzzles Solved: " + playerDefPuzSolved;

        // during attack phase (and game still going on)
        if (!timer.checkPuzzlePhase() && stageWin == 0)
        {
            // enemy AI calculate dmg 
            enemyAI();

            if (timer.checkAnimationTriggered("player"))
            {
                StartCoroutine(AttackEnemy());
                player.doHitAnimation();
            }

            if (timer.checkAnimationTriggered("enemy"))
            {
                StartCoroutine(GetAttacked());
                enemies[0].doHitAnimation();
            }

            // check if game is over or not. if not, reset stats for next round
            if (checkGameOver() == 0)
            {
                startNextRound();
            }
        }

	}

    // this function should have a good algorithm to calculate a random dmg and atk 
    // appropriate for the level of the stage. 
    private void enemyAI()
    {
        enemyAtkPuzSolved = Random.Range(5, 10);
        enemyDefPuzSolved = Random.Range(0, 3);
    }

    IEnumerator AttackEnemy()
    {
        // calculate damage received
        int enemyDmgReceived = calculateDmg(playerAtkPuzSolved * player.getStrength(),
                                            enemyDefPuzSolved * enemies[0].getDefence());

        // deal damage to enemy
        enemyDmgedText.text = (enemyDmgReceived) + "!"; // show dmg text
        enemies[0].receiveDamage(enemyDmgReceived); // update health
        // hold for a while
        Invoke("HideDamageEnemy", 2f);
        yield return new WaitForSeconds(2f);
    }

    IEnumerator GetAttacked()
    {
        // calculate damage received
        int playerDmgReceived = calculateDmg(enemyAtkPuzSolved * enemies[0].getStrength(),
                                             playerDefPuzSolved * player.getDefence());

        // receive damage 
        playerDmgedText.text = playerDmgReceived + "!"; // show dmg text
        player.receiveDamage(playerDmgReceived); // update health
        // hold for a while
        Invoke("HideDamagePlayer", 2f);
        yield return new WaitForSeconds(2f);
    }

    private void HideDamageEnemy()
    {
        enemyDmgedText.text = "";
    }

    private void HideDamagePlayer()
    {
        playerDmgedText.text = "";
    }

    // do damage calculation here 
    private int calculateDmg(int atk, int def)
    {
        int dmg = atk - def;
        if (dmg < 0)
        {
            dmg = 0;
        }
        return dmg;
    }
    
    // check if either player or enemy is dead
    // returns -1 for lose, 1 for win, 0 for no one dead
    private int checkGameOver()
    {
        if (enemies[0].checkDead())
        {
            stageWin = 1;
            winText.text = "YOU WIN!";
            timer.stopTimer();
        }
        else if (player.checkDead())
        {
            stageWin = -1;
            winText.text = "DEFEATED!";
            timer.stopTimer();
        }
        else
        {
            stageWin = 0;
        }
        return stageWin;
    }

    public static int getLevel()
    {
        return level;
    }
}


