using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIManager : MonoBehaviour {

    private const int MAX_PUZZLE_POINT = 6;
    private const float DEF_UI_HEIGHT = 2.35f;
    private const float ATK_UI_HEIGHT = 2.55f;
    private const float HP_UI_HEIGHT = 4.0f;

    public GameObject attackIcon;
    public GameObject defendIcon;
    public GameObject healthIcon;
    public GameObject attackBox;
    public GameObject defendBox;

    public GameObject attackPoint;
    public GameObject defendPoint;
    public GameObject healthBar;

    private Text playerAttackText;
    private Text playerDefendText;
    private Text playerHealthText;
    private Text enemyAttackText;
    private Text enemyDefendText;
    private Text enemyHealthText;

    static private int playerAtk, playerDef, playerHp, enemyAtk, enemyDef, enemyHp;

    // Use this for creating the important icons
    public void Setup() {
        // creating player's UI icons
        Instantiate(attackIcon, new Vector3(0.9f, ATK_UI_HEIGHT, 0f), Quaternion.identity);
        Instantiate(defendIcon, new Vector3(0.9f, DEF_UI_HEIGHT, 0f), Quaternion.identity);
        Instantiate(healthIcon, new Vector3(0.9f, HP_UI_HEIGHT, 0f), Quaternion.identity);
        Instantiate(healthBar, new Vector3(1.5f, HP_UI_HEIGHT, 0f), Quaternion.identity);

        for (int i = 0; i < MAX_PUZZLE_POINT; i++)
        {
            Instantiate(attackBox, new Vector3(1.4f + (i*0.18f), ATK_UI_HEIGHT, 0f), Quaternion.identity);
            Instantiate(defendBox, new Vector3(1.4f + (i * 0.18f), DEF_UI_HEIGHT, 0f), Quaternion.identity);
        }

        // creating enemy's UI icons
        Instantiate(attackIcon, new Vector3(3.7f, ATK_UI_HEIGHT, 0f), Quaternion.identity);
        Instantiate(defendIcon, new Vector3(3.7f, DEF_UI_HEIGHT, 0f), Quaternion.identity);
        Instantiate(healthIcon, new Vector3(3.7f, 4.4f, 0f), Quaternion.identity);
        Instantiate(healthBar, new Vector3(4.3f, 4.4f, 0f), Quaternion.identity);

        for (int i = 0; i < MAX_PUZZLE_POINT; i++)
        {
            Instantiate(attackBox, new Vector3(4.2f + (i * 0.18f), ATK_UI_HEIGHT, 0f), Quaternion.identity);
            Instantiate(defendBox, new Vector3(4.2f + (i * 0.18f), DEF_UI_HEIGHT, 0f), Quaternion.identity);
        }

        // getting corresponding UI text
        playerAttackText = GameObject.Find("PlayerStrength").GetComponent<Text>();
        playerDefendText = GameObject.Find("PlayerDefence").GetComponent<Text>();
        playerHealthText = GameObject.Find("PlayerHealth").GetComponent<Text>();
        enemyAttackText = GameObject.Find("EnemyStrength").GetComponent<Text>();
        enemyDefendText = GameObject.Find("EnemyDefence").GetComponent<Text>();
        enemyHealthText = GameObject.Find("EnemyHealth").GetComponent<Text>();
       
    }

    // use these function to initialize the values
    public static void InitPlayerStats(int atk, int def, int hp)
    {
        playerAtk = atk;
        playerDef = def;
        playerHp = hp;
    }

    public static void InitEnemyValues(int atk, int def, int hp)
    {
        enemyAtk = atk;
        enemyDef = def;
        enemyHp = hp;
    }

    // use these functions to update the number of puzzles solved
    public void UpdateAtkPuzzleSolved(int num)
    {
        Instantiate(attackPoint, new Vector3(1.4f + ((num -1) * 0.18f), ATK_UI_HEIGHT, 0f), Quaternion.identity);
    }

    public void UpdateDefPuzzleSolved(int num)
    {
        Instantiate(defendPoint, new Vector3(1.4f + ((num -1) * 0.18f), DEF_UI_HEIGHT, 0f), Quaternion.identity);
    }

    public void UpdateEnemyAtkPuzSolved(int num)
    {
        Instantiate(attackPoint, new Vector3(4.2f + ((num - 1) * 0.18f), ATK_UI_HEIGHT, 0f), Quaternion.identity);
    }

    public void UpdateEnemyDefPuzSolved(int num)
    {
        Instantiate(defendPoint, new Vector3(4.2f + ((num - 1) * 0.18f), DEF_UI_HEIGHT, 0f), Quaternion.identity);
    }

    // use this to clear out all points gained
    public void ClearPoints()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Points");
        foreach (GameObject point in points)
        {
            Destroy(point);
        }
    }

    // use these functions to update health 
    public static void UpdatePlayerHealth(int hp)
    {
        playerHp = hp;
    }

    public static void UpdateEnemyHealth(int hp)
    {
        enemyHp = hp;
    }
	
	// Update is called once per frame
	void Update () {
        playerAttackText.text = "" + playerAtk;
        playerDefendText.text = "" + playerDef;
        playerHealthText.text = "" + playerHp;
        enemyAttackText.text = "" + enemyAtk;
        enemyDefendText.text = "" + enemyDef;
        enemyHealthText.text = "" + enemyHp;
    }
}
