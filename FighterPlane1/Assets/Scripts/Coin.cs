using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue;
    public AudioClip soundToPlay;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // FIXED
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (!whatDidIHit.CompareTag("Player")) return;

        // Add coins
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().AddScore(coinValue);

        // Play sound safely
        PlayOneShotSound(soundToPlay);

        // Destroy only the sprite, not the sound
        Destroy(gameObject);
    }

    public void PlayOneShotSound(AudioClip clip)
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
        tempAS.Play();
        Destroy(tempAudio, clip.length);
    }
}
