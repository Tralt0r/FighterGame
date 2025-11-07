using UnityEngine;

public class BulletOverlap : MonoBehaviour
{
    public GameObject explosionPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
            if (whatDidIHit.tag == "Player")
            {
                whatDidIHit.GetComponent<PlayerController>().LoseALife();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
    }
}
