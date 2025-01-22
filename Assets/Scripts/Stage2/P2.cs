using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class P2 : MonoBehaviour
{
    public float moveSpeedX;
    public float moveSpeedY;

    void Start()
    {
        
    }

    void Update()
    {
        bool isPressingUp = Input.GetKey(KeyCode.W);
        bool isPressingDown = Input.GetKey(KeyCode.S);
        bool isPressingLeft = Input.GetKey(KeyCode.A);
        bool isPressingRight = Input.GetKey(KeyCode.D);

        if(isPressingUp)
        {
            transform.Translate(Vector2.up * Time.deltaTime * moveSpeedY);
        }

        if(isPressingDown)
        {
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeedY);
        }
        if(isPressingLeft)
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeedX);
        }

        if(isPressingRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeedX);
        }
        float clampedX = Mathf.Clamp(transform.position.x, -11f, -3f);
        float clampedY = Mathf.Clamp(transform.position.y, -6f, 6f);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
