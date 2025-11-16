using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue;

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.CompareTag("Player"))
        {
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().AddScore(coinValue);
            Destroy(gameObject);
        }
        else return;
    }
}
