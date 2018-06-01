using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.
    private BoardManager boardScript;           // script that creates bg 
    private PuzzleManager puzzleManagerScript;             // script that handles puzzle switching and score keeping
    private Formulas formulasScript;            // script that holds all the formulas for RPG calculation
    private Player player;                      // hold referrence to player script
    private List<Enemy> enemies;                // hold referrence to enemies script
    private SplashScreen splashScript;          // hold referrence to splash screen script
    private Timer timer;                        // hold referrence to timer script
   
    public static int level;      // used to keep track of stage 
    private int stageWin; // -1 for lose, 0 for ongoing, 1 for win
    
    private Text enemyDmgedText;
    private Text playerDmgedText;
    private Text winText;

    private bool rewardOnceOnly = false;

    public GameObject[] tutorials; // list of all tutorials 
    private static GameObject tutorial; // current showing tutorial, if any

    public GameObject warningLogo; // just the boss level warning logo
    public GameObject winningScreen; // winning screen animation

    // Use this for initialization
    void Awake () {
        if (instance == null){ // check if instance already exists
            instance = this;    // if not, set instance to this
        }

        else if (instance != this){ // if instance already exists and is not this
            Destroy(gameObject);    // then destroy it. enforcing singleton
        }

        enemies = new List<Enemy>();    // allocate memory for enemies 

        boardScript = GetComponent<BoardManager>();
        puzzleManagerScript = GetComponent<PuzzleManager>();
        formulasScript = GetComponent<Formulas>();
        timer = GetComponent<Timer>();
        level = PlayerPrefs.GetInt("stageSelected");

        if (level == 10) // if boss level, create warning sign
        {
            Instantiate(warningLogo, new Vector3(3f, 0f, 0f), Quaternion.identity);
        }

        InitGame();
	}

    void Start ()
    {
        
    }

    // set or reset stats to prepare for next round
    private void startNextRound()
    {
        puzzleManagerScript.ResetScore();
    }

    private void InitGame()
    {
        stageWin = 0;
        
        enemyDmgedText = GameObject.Find("EnemyDamagedText").GetComponent<Text>();
        playerDmgedText = GameObject.Find("PlayerDamagedText").GetComponent<Text>();
        winText = GameObject.Find("YouWinText").GetComponent<Text>();

        enemies.Clear(); // clear monster in list to prepare for next level

        boardScript.SetupScene(level);  // setup bg
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

    public void AddSplashScreenToManager(SplashScreen script)
    {
        splashScript = script;
    }

    // Update is called once per frame
    void Update () {
        
        // during puzzle phase
        if (Timer.checkPuzzlePhase())
        {
            enemies[0].SetIsTurn(true); // allow enemy to start solving puzzles
        }

        // when not during puzzle phase (and game still going on)
        if (!Timer.checkPuzzlePhase() && stageWin == 0)
        {
            enemies[0].SetIsTurn(false); // stop enemy from solving puzzles

            // check with timer if it's time to play certain animations
            if (Timer.checkAnimationTriggered("splashPuzzle"))
            {
                splashScript.SplashPuzzleScreen(); 
            }

            if (Timer.checkAnimationTriggered("splashAttack"))
            {
                splashScript.SplashAttackScreen();
            }

            if (Timer.checkAnimationTriggered("player"))
            {
                StartCoroutine(AttackEnemy());
                player.DoHitAnimation();
                enemies[0].DoDmgedAnimation();
            }

            if (Timer.checkAnimationTriggered("enemy"))
            {
                StartCoroutine(GetAttacked());
                enemies[0].DoHitAnimation();
                player.DoDmgedAnimation();
            }

            if (Timer.checkAnimationTriggered("bossAppearance"))
            {
                enemies[0].BossAppearance();
                Destroy(GameObject.FindGameObjectWithTag("Warning"));
            }
            
        }

        // check if game is over or not. if not, reset stats for next round
        if (checkGameOver() == 0 && Timer.checkReadyForNextRound())
        {
            startNextRound();
        }

    }

    IEnumerator AttackEnemy()
    {
        // calculate damage dealt to enemy
        int enemyDmgReceived = formulasScript.calculateDmg(
                            puzzleManagerScript.GetPlayerAtkScore() * player.GetStrength(),
                            puzzleManagerScript.GetEnemyDefScore() * enemies[0].GetDefence());

        // deal damage to enemy
        enemyDmgedText.text = (enemyDmgReceived) + "!"; // show dmg text
        enemies[0].ReceiveDamage(enemyDmgReceived); // update health
        // hold for a while
        Invoke("HideDamageEnemy", 2f);
        yield return new WaitForSeconds(2f);
    }

    IEnumerator GetAttacked()
    {
        // calculate damage received
        int playerDmgReceived = formulasScript.calculateDmg(
                                puzzleManagerScript.GetEnemyAtkScore() * enemies[0].GetStrength(),
                                puzzleManagerScript.GetPlayerDefScore() * player.GetDefence());

        // receive damage 
        playerDmgedText.text = playerDmgReceived + "!"; // show dmg text
        player.ReceiveDamage(playerDmgReceived); // update health
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
     
    // check if either player or enemy is dead
    // returns -1 for lose, 1 for win, 0 for no one dead
    private int checkGameOver()
    {
        if (enemies[0].CheckDead())
        { 
            stageWin = 1;
            winText.text = "YOU WIN!";
            
            // ADD PANEL HERE
            if (rewardOnceOnly == false)
            {
                player.GainExp(formulasScript.calculateExpGain(level));
                PlayerPrefs.SetInt("stageUnlocked", level+1);
                rewardOnceOnly = true;
            }         
            if (level != 10)
            {
                GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(14).gameObject.SetActive(true);
                Timer.stopTimer();
            }
            else 
            {
                // BOSS DIE HERE 
                timer.GameWin();
                if (Timer.checkAnimationTriggered("bossDie"))
                {
                    enemies[0].BossDie();
                }
                if (Timer.checkAnimationTriggered("gameWon"))
                {
                    Instantiate(winningScreen); // show winning screen
                }
            }
        }
        else if (player.CheckDead())
        {    
            stageWin = -1;
            winText.text = "DEFEATED!";
            Timer.stopTimer();
            // ADD PANEL HERE
            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(13).gameObject.SetActive(true);
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

    public void showTutorial(int number)
    {
        tutorial = tutorials[number - 1];
        Instantiate(tutorial);
    }
    
    public void closeTutorial(int number)
    {
        timer.startPuzzle();
        tutorial = GameObject.FindGameObjectWithTag("Tutorial");
        Destroy(tutorial);
    }

}


