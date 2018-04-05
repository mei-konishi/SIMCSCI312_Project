using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitchButton : MonoBehaviour
{

    public int thisButtonNumber;

    private PuzzleManager puzzleManager;

    // Use this for initialization
    void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    private void OnMouseDown()
    {
        // set clicked button as currently active
        puzzleManager.SetNewActive(thisButtonNumber);
    }

    private void OnMouseUp()
    {

    }

}
