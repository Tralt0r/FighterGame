using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{
    [Header("Movement Settings")]
    public bool goingUp;
    public bool swingingSideToSide;
    [Header("Vertical Movement Settings")]
    public float speed;
    [Header("Horizontal Movement Settings")]
    public float horizontalspeed;
    public float horizontalRangeMIN;
    public float horizontalRangeMAX;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if (goingUp == false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (transform.position.y >= gameManager.verticalScreenSize * 1.25f || transform.position.y <= -gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(this.gameObject);
        }
        if (swingingSideToSide)
        {
            transform.Translate(Vector3.right * horizontalspeed * Time.deltaTime);
            if (transform.position.x >= horizontalRangeMAX)
            {
                swingingSideToSide = false;
            }
        }
        else if (swingingSideToSide == false)
        {
            transform.Translate(Vector3.left * horizontalspeed * Time.deltaTime);
            if (transform.position.x <= horizontalRangeMIN)
            {
                swingingSideToSide = true;
            }
        }
    }
    
}
