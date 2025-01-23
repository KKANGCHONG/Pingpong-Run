using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(transform.position.x < -15 || transform.position.x > 15)  {
    //         Destroy(gameObject);
    //     }
    // }

    void Update()
    {
        if(transform.position.x < -15 && !CompareTag("ball"))  {
            Destroy(gameObject);
        }
        if(transform.position.x > 15 && CompareTag("ball"))  {
            Destroy(gameObject);
        }
    }
}
