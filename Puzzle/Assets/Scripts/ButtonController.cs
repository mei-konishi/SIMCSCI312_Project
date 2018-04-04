using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer theSprite;

    public int thisButtonNumber;

    private GameManagers theGM;

    private AudioSource theSound;

    // Use this for initialization
    void Start()
    {
        theSprite = GetComponent<SpriteRenderer>();
        theGM = FindObjectOfType<GameManagers>();
        theSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        theSprite.color = new Color(theSprite.color.r, theSprite.color.g, theSprite.color.b, 1f);
        theSound.Play();
    }

    private void OnMouseUp()
    {
        theSprite.color = new Color(theSprite.color.r, theSprite.color.g, theSprite.color.b, 0.5f);

        if (!theGM.stop)
        {
            theGM.ColourPressed(thisButtonNumber);
        }
        
        theSound.Stop();
    }
}

