using UnityEngine;

public class TileMove : MonoBehaviour
{
    public float tileSpeed = 5f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(-tileSpeed * Time.deltaTime, 0, 0);
    }
}
