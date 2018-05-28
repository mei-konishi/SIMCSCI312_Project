using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour {

    AudioSource fxSound; 
    public AudioClip backMusic;
                                // Use this for initialization
    void Start()
    {
        // Audio Source responsavel por emitir os sons
        fxSound = GetComponent<AudioSource>();
        fxSound.Play();
    }

}
