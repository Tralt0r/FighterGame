using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives;
    private float speed;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public float fireRate = 1000f; // seconds between shots
    private float nextFireTime = 0.1f;

    private float minX, maxX, minY, maxY;
    private Camera cam;
    private bool canfire = true;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;

        minY = screenBottomLeft.y;
        maxY = screenTopRight.y;

        maxY = (minY + maxY) / 4f;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 8.0f;
        gameManager.ChangeLivesText(lives);
        Debug.Log("Lives at start: " + lives);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 newPos = transform.position + new Vector3(moveX, moveY, 0);

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        transform.position = newPos;

        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        //lives = lives - 1;
        //lives -= 1;
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void Shooting()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            if (canfire)
                canfire = false;
                Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                nextFireTime = Time.time + fireRate;
                StartCoroutine(DelayedAction(fireRate));
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y <= -verticalScreenSize || transform.position.y > verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }

    }

    IEnumerator DelayedAction(float delayTime)
    {
        // Wait for the specified number of seconds (affected by Time.timeScale)
        yield return new WaitForSeconds(delayTime);

        canfire = true;
        Debug.Log("Can Fire Again");
    }

    
}
