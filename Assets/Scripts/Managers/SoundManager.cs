using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip diamondCollectSound;
    public AudioClip backgroundMusic;

    private Vector3 cameraPosition;
    private AudioSource musicSource;


    void Awake()
    {
        Instance = this;
        cameraPosition = Camera.main.transform.position;

        // Set up background music source
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = 0.5f;
    }

    private void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, cameraPosition);
    }

    public void PlayDiamondCollectSound()
    {
        PlaySound(diamondCollectSound);
    }

    public void PlayBackgroundMusic()
    {
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
    }

    void Update()
    {

    }
}
