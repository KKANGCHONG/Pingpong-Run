using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemMove : MonoBehaviour
{
    public float itemSpeed = 5f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(-itemSpeed * Time.deltaTime, 0, 0);
    }
}
