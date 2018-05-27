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
   
    public static int level = 1;      // used to keep track of stage 
    private int stageWin; // -1 for lose, 0 for ongoing, 1 for win
    
    private Text enemyDmgedText;
    private Text playerDmgedText;
    private Text winText;

    // Win / Lose Panels 
    public GameObject canvas;

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

        InitGame();
	}

    void Start ()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        //youWinPanel.SetActive(false);
        //youLosePanel.SetActive(false);
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

    // Update is called once per frame
    void Update () {
        
        // during attack phase (and game still going on)
        if (!Timer.checkPuzzlePhase() && stageWin == 0)
        {
            // enemy AI calculate dmg 
            enemyAI();

            if (Timer.checkAnimationTriggered("player"))
            {
                StartCoroutine(AttackEnemy());
                player.doHitAnimation();
            }

            if (Timer.checkAnimationTriggered("enemy"))
            {
                StartCoroutine(GetAttacked());
                enemies[0].doHitAnimation();
            }
        }

        // check if game is over or not. if not, reset stats for next round
        if (checkGameOver() == 0 && Timer.checkReadyForNextRound())
        {
            startNextRound();
        }

    }

    // this function should have a good algorithm to calculate a random dmg and atk 
    // appropriate for the level of the stage. 
    private void enemyAI()
    {
        puzzleManagerScript.EnemyAI();
    }

    IEnumerator AttackEnemy()
    {
        // calculate damage dealt to enemy
        int enemyDmgReceived = formulasScript.calculateDmg(
                            puzzleManagerScript.GetPlayerAtkScore() * player.getStrength(),
                            puzzleManagerScript.GetEnemyDefScore() * enemies[0].getDefence());

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
        int playerDmgReceived = formulasScript.calculateDmg(
                                puzzleManagerScript.GetEnemyAtkScore() * enemies[0].getStrength(),
                                puzzleManagerScript.GetPlayerDefScore() * player.getDefence());

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
    
    
    // check if either player or enemy is dead
    // returns -1 for lose, 1 for win, 0 for no one dead
    private int checkGameOver()
    {
        if (enemies[0].checkDead())
        {          
            stageWin = 1;
            winText.text = "YOU WIN!";
            Timer.stopTimer();
            // ADD PANEL HERE?
            canvas.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (player.checkDead())
        {         
            stageWin = -1;
            winText.text = "DEFEATED!";
            Timer.stopTimer();
            // ADD PANEL HERE?
            canvas.transform.GetChild(1).gameObject.SetActive(true);
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


