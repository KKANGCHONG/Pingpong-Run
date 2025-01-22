using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallMobe : MonoBehaviour
{
    public float ballSpeed = 5f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(-ballSpeed * Time.deltaTime, 0, 0);
    }
}
