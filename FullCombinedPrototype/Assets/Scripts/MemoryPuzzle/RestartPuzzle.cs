using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPuzzle : MonoBehaviour
{

    [SerializeField] private MemoryPuzzleSceneController controller;
    // Use this for initialization
    void Start()
    {
        Destroy(controller);
        controller.Restart();
    }

    // Update is called once per frame
    void Update()
    {

    }

}