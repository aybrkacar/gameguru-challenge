using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip StartingNote;
    public float StartingPitch = 1.0f;

    public float PitchIncreaseAmount = 0.1f;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = StartingNote;
        //_audioSource.pitch = StartingPitch;
    }


    public void PlaySound(bool isPerfect)
    {
        Debug.Log("Oynadı");
        if(isPerfect){
             _audioSource.pitch += PitchIncreaseAmount;
        }
        else{
            _audioSource.pitch = StartingPitch;
        }

        _audioSource.Play();
       
    }
}
