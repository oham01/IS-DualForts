using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip diamondCollectSound;

    private Vector3 cameraPosition;

    void Awake()
    {
        Instance = this; 
        cameraPosition = Camera.main.transform.position; 

    }

    private void PlaySound(AudioClip clip) 
    {
        AudioSource.PlayClipAtPoint(clip, cameraPosition); 
    }

    public void PlayDiamondCollectSound()
    {
        PlaySound(diamondCollectSound);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
