using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip soundToPlay;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // FIXED
        PlayOneShotSound(soundToPlay);
        Destroy(this.gameObject, 2.5f);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOneShotSound(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning("Audio clip is missing!");
            return;
        }

        // Create a temporary object to play the sound
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource tempAS = tempAudio.AddComponent<AudioSource>();
        tempAS.clip = clip;
        tempAS.volume = 0.5f; // set desired volume
        tempAS.Play();
        Destroy(tempAudio, clip.length);
    }
}
