using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float mapSpeed = 5f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(-mapSpeed * Time.deltaTime, 0, 0);
    }
}
