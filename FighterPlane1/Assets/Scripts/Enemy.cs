using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Enemy : MonoBehaviour
{

    public GameObject explosionPrefab;
    public float timeShootMIN;
    public float timeShootMAX;
    public float shootInterval;
    public bool canShoot;
    public int teamID = 0; // e.g. 0 = player, 1 = enemies


    private GameManager gameManager;
    public GameObject bulletPrefab;
    public List<GameObject> EnemiestoAvoid;

    public int points = 10;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
         if (canShoot)
         {
            canShoot = false;
            shootInterval = Random.Range(timeShootMIN, timeShootMAX);
            Invoke("Shoot", shootInterval);
            
         }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.CompareTag("Player"))
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (whatDidIHit.CompareTag("Weapons"))
        {
            BulletOwner bulletOwner = whatDidIHit.GetComponent<BulletOwner>();

            // Ignore bullets from same team
            if (bulletOwner != null && bulletOwner.teamID == teamID)
            {
                return; // skip friendly fire
            }

            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(gameObject);
        }

    }
    
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
        BulletOwner bulletOwner = bullet.GetComponent<BulletOwner>();
        if (bulletOwner != null)
        {
            bulletOwner.teamID = teamID; // pass the team
        }

        canShoot = true;
    }
    void OnDestroy()
    {
        ScoreManager scoreManager = UnityEngine.Object.FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(points);
        }
    }
}
