using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public AudioClip soundToPlay1;
    public AudioClip soundToPlay2;
    private AudioSource audioSource;

    public float growDuration = 0.3f;   // how fast the shield grows
    public float scaleMultiplier = 30f;  // grow 4x

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (!whatDidIHit.CompareTag("Player")) return;

        // Attach to player
        transform.SetParent(whatDidIHit.transform);

        // Move shield to center of player
        transform.localPosition = Vector3.zero;

        // Adjust if sprite pivot isn't centered
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Vector3 offset = sr.bounds.center - transform.position;
            transform.localPosition -= offset;
        }

        // Disable shield collider and glider script
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Glider>().enabled = false;

        // Disable player's collider
        Collider2D playerCollider = whatDidIHit.GetComponent<Collider2D>();
        if (playerCollider != null)
            playerCollider.enabled = false;

        // Play pickup sound
        PlayOneShotSound(soundToPlay1);

        // Begin scale-up animation
        StartCoroutine(GrowShield());

        // Begin delay before removal
        StartCoroutine(HandleDelay(playerCollider));
    }

    // Smoothly scale the shield up by 4x
    private IEnumerator GrowShield()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * scaleMultiplier;

        float t = 0f;

        while (t < growDuration)
        {
            t += Time.deltaTime;
            float lerp = t / growDuration;

            transform.localScale = Vector3.Lerp(startScale, endScale, lerp);
            yield return null;
        }

        // Guarantee final value
        transform.localScale = endScale;
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

    private IEnumerator HandleDelay(Collider2D playerCollider)
    {
        // Wait 5 seconds
        yield return new WaitForSeconds(4f);
        PlayOneShotSound(soundToPlay2);

        yield return new WaitForSeconds(1f);

        // Re-enable player collider
        if (playerCollider != null)
            playerCollider.enabled = true;

        // Play shield break sound
        

        // Destroy shield
        Destroy(gameObject);
    }
}
