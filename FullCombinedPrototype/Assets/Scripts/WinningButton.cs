using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningButton : MonoBehaviour {

    void OnMouseDown()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
