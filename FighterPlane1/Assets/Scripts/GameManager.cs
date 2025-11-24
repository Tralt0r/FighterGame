using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject coinPrefab;
    public GameObject cloudPrefab;
    public GameObject shieldPrefab;
    public GameObject powerUpPrefab;
    public GameObject lifePrefab;

    public TextMeshProUGUI livesText;

    public float horizontalScreenSize;
    private int chosenEnemy;
    public float verticalScreenSize;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemy1", 1, 2);
        InvokeRepeating("CreateEnemy2", 1, 0.5f);
        InvokeRepeating("CreateCoins", 4, 2);
        InvokeRepeating("CreateShield", 30, 60);
        InvokeRepeating("CreatePowerUp", 1, 45);
        InvokeRepeating("CreateLife", 1, 45);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy1()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }
    void CreateEnemy2()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }
    void CreateCoins()
    {
        for (int i = 0; i < 4; i++)
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
        Task.Delay(500);
    }
    void CreateShield()
    {

        Instantiate(shieldPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }
    void CreatePowerUp()
    {
        Instantiate(powerUpPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }
    void CreateLife()
    {
        Instantiate(lifePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }
    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        
    }
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    public IEnumerator DeathResetTimer()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game");
    }
}
