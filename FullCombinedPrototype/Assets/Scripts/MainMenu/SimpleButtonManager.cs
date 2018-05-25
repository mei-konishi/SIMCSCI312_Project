using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleButtonManager : MonoBehaviour {

    public GameObject pauseMenu;

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void togglePanel()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
