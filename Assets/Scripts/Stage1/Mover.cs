using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CompareTag("ball"))  {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        else    {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
    }
}
