using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public AudioClip soundToPlay1;
    public AudioClip soundToPlay2;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (!whatDidIHit.CompareTag("Player")) return;

        PlayerController player = whatDidIHit.GetComponent<PlayerController>();
        if (player != null)
            player.ActivateTripleShot();
        // Play pickup sound
        PlayOneShotSound(soundToPlay1);
        Destroy(gameObject);
    }

    public void PlayOneShotSound(AudioClip clip)
    {
        if (clip == null) return;

        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource tempAS = tempAudio.AddComponent<AudioSource>();
        tempAS.clip = clip;
        tempAS.Play();

        Destroy(tempAudio, clip.length);
    }
}
