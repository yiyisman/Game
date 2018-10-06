using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{

    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource audios;
    public bool alreadyPlayed = false;
    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        if (!alreadyPlayed)
        {
            audios.PlayOneShot(SoundToPlay, Volume);
            alreadyPlayed = true;
        }
    }
}