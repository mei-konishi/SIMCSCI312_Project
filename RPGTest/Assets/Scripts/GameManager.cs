using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.
    private BoardManager boardScript;           // create background
    public GameObject playerSprite;           // create player image
    private Player player;                      // hold referrence to player script
    private List<Enemy> enemies;                // hold referrence to enemies script
    
    //   public Timer timer;

    private static int level = 1;      // used to keep track of stage 
    private bool puzzlePhase;
    private int stageWin; // -1 for lose, 0 for ongoing, 1 for win

    const float timerDuration = 5.0f;
    // temp timer for rpg testing
    float timeLeft;
    private Text timerText;
    public float turnDelay = 2f;

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
        player = new Player();          // allocate memory for Player

        boardScript = GetComponent<BoardManager>();
        
        timeLeft = timerDuration;

        InitGame();
	}

    // set or reset stats to prepare for next round
    private void startNextRound()
    {
        playerAtkPuzSolved = 0;
        playerDefPuzSolved = 0;
        enemyAtkPuzSolved = 0;
        enemyDefPuzSolved = 0;
        timeLeft = timerDuration;
        puzzlePhase = true;
    }

    private void InitGame()
    {
        puzzlePhase = false;    // game has not begun yet
        stageWin = 0; 

        timerText = GameObject.Find("Timer").GetComponent<Text>();
        puzzleSolvedText = GameObject.Find("PuzzleSolvedText").GetComponent<Text>();
        enemyDmgedText = GameObject.Find("EnemyDamagedText").GetComponent<Text>();
        playerDmgedText = GameObject.Find("PlayerDamagedText").GetComponent<Text>();
        winText = GameObject.Find("YouWinText").GetComponent<Text>();

        enemies.Clear(); // clear monster in list to prepare for next level

        boardScript.SetupScene(level);  // setup bg

        // create player sprite
        Instantiate(playerSprite, new Vector3(0.5f, 3, 0f), Quaternion.identity);

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

    // when timer sends message to this controller that time has passed
    public void timeElapsed()
    {
        puzzlePhase = false; // end the puzzle phase
    }

    private void puzzleComplete (int atkScore, int defScore)
    {

    }

    static public void callBackButtonPress (string name)
    {

    }

    // Update is called once per frame
    void Update () {
        
        // during puzzle phase
		if (puzzlePhase)
        {
            // count down timer for puzzle phase
            timerText.text = "Timer : " + ((int)timeLeft + 1);
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timerText.text = "Attacking";
                puzzlePhase = false;
                timeLeft = timerDuration;
            }

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
        if (!puzzlePhase && stageWin == 0)
        {
            // enemy AI calculate dmg 
            enemyAI();

            // trying to test out a delay to make it look "animated" 
            // trying to make player attack first, then enemy attack, but not working now
            StartCoroutine(AttackEnemy());

            StartCoroutine(GetAttacked());
            
            // somehow animation not triggering.
            player.doHitAnimation();

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
        int enemyDmgReceived = calculateDmg(playerAtkPuzSolved, enemyDefPuzSolved);

        // deal damage to enemy
        enemyDmgedText.text = (enemyDmgReceived) + "!"; // show dmg text
        enemies[0].receiveDamage(enemyDmgReceived); // update health
        // hold for a while
        Invoke("HideDamageEnemy", turnDelay);
        yield return new WaitForSeconds(turnDelay);
    }

    IEnumerator GetAttacked()
    {
        // calculate damage received
        int playerDmgReceived = calculateDmg(enemyAtkPuzSolved, playerDefPuzSolved);

        // receive damage 
        playerDmgedText.text = playerDmgReceived + "!"; // show dmg text
        player.receiveDamage(playerDmgReceived); // update health
        // hold for a while
        Invoke("HideDamagePlayer", turnDelay);
        yield return new WaitForSeconds(turnDelay);
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
        }
        else if (player.checkDead())
        {
            stageWin = -1;
            winText.text = "DEFEATED!";
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


