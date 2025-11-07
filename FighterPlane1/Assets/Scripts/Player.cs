using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float playerSpeed = 6f;
    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;
    private float verticalScreenLimit = 6.5f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public float fireRate = 0.2f; // seconds between shots
    private float nextFireTime = 0f;

    void Update()
    {
        Movement();
        Shooting();
    }

    void Shooting()
    {
        // Continuous fire while holding Space
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        // Screen wrap horizontally
        if (transform.position.x > horizontalScreenLimit || transform.position.x < -horizontalScreenLimit)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
        }

        // Screen wrap vertically
        if (transform.position.y > verticalScreenLimit || transform.position.y < -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, 0);
        }
    }
}
