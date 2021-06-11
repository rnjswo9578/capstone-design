using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_playerSounds : MonoBehaviour
{
    public AudioClip audioAttack;
    public AudioClip audioDash;
    public AudioClip audioMove;
    public AudioClip audioDamaged;
    public AudioClip audioDie;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string action){
        switch (action)
        {
            case "Attack":
                audioSource.clip = audioAttack;
                break;
            case "Dash":
                audioSource.clip = audioDash;
                break;
            case "Move":
                audioSource.clip = audioMove;
                break;
            case "Damaged":
                audioSource.clip = audioDamaged;
                break;
            case "Die":
                audioSource.clip = audioDie;
                break;
        }
        audioSource.Play();
    }

}
