



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    PlayerControls playerLink;
    AudioSource audioLink;

    void Start()
    {
        audioLink = GetComponent<AudioSource>();
        playerLink = FindObjectOfType<PlayerControls>();
    }

    
    void Update()
    {
        if (playerLink.Mute == true)
        {
            audioLink.mute = true;
        }
    }
   
}
