using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public List<AudioClip> randomSoundSpaces = new List<AudioClip>();
    public float timeBetweenSound;
    float timeSinceLastSound;

    public AudioSource audioSource;

    void Update(){
        if(timeSinceLastSound > timeBetweenSound){
            PlayerRandomSound();
            timeSinceLastSound = 0;
        }
        timeSinceLastSound += Time.deltaTime;
    }

    void PlayerRandomSound(){
        audioSource.PlayOneShot(randomSoundSpaces[Random.Range((int)0,randomSoundSpaces.Count)],0.2f);
    }   
}
