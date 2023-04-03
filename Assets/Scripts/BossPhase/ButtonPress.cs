using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public AudioSource audio;

    public void playButton() 
    {
        audio.Play();
    }
}
