using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float enemySpeed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(-enemySpeed * Time.deltaTime, 0, 0);
    }
}
