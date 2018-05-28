using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    // for background generation 
    private int columns = 8;
    private int rows = 5;
    
    private Transform boardHolder;

    public GameObject[] backgrounds;
    public GameObject[] floorTiles;
    public GameObject[] enemyTiles;
    public GameObject[] winPanels;
    public GameObject losePanel;

    public void SetupScene (int level)
    {
        //bgSetup(); // dynamic tile generation. was just testing. don't need it in our project

        //createEnemies(level);

        // create background for level
        Instantiate(backgrounds[level -1], new Vector3(3.0f, 3.5f, 0), Quaternion.identity);

        // create enemy for level
        Instantiate(enemyTiles[level -1], new Vector3(4.5f, 3.4f, 0f), Quaternion.identity);

        // create panels for level
        GameObject theWinPanel = Instantiate(winPanels[level - 1], new Vector3(0f, 0f, 0f), Quaternion.identity);
        GameObject theLosePanel = Instantiate(losePanel, new Vector3(0f, 0f, 0f), Quaternion.identity);
        theWinPanel.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        theLosePanel.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        theWinPanel.transform.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(() => LoadLevel("StartMenu"));
        theLosePanel.transform.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(() => LoadLevel("StartMenu"));
    }


    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    /*
        private void bgSetup()
        {
            boardHolder = new GameObject("bg").transform;

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y + 1.5f, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }

        private void createEnemies(int level)
        {
            for(int i = 0; i <= level; i += 2)
            {
                // create an enemy (just 1 type for prototype for now) 
                Instantiate(enemyTiles[0], new Vector3(4.5f, 3.5f, 0f), Quaternion.identity);
            }
        }*/
}
