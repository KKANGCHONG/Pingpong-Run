using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float startingSpeed;
    void Start()
    {
        bool isRight = UnityEngine.Random.value >= 0.5;
        float xVelocity = -1f;

        if(isRight == true) 
        {
            xVelocity = 1f;
        }

        float yVelocity = UnityEngine.Random.Range(-1,1);

        // rb.velocity = new Vector2(xVelocity, yVelocity);
        rb.linearVelocity = new Vector2(xVelocity * startingSpeed, yVelocity * startingSpeed);
    }

    void Update()
    {
        
    }
}
