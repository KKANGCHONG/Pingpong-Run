using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public float stopXPosition = 6f; // 멈출 x좌표

    private bool isStopped = false;
    private System.Action<Vector3> stopCallback;
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
        else if (CompareTag("stair"))
        {
            if (!isStopped) {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;

                if (transform.position.x <= stopXPosition)  {
                    isStopped = true;
                    stopCallback?.Invoke(transform.position); // 멈춤 위치 전달
                }
                else    {
                    transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                }
            }
        }
        else    {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
    }

    public void SetStopCallback(System.Action<Vector3> callback) {
        stopCallback = callback;
    }
}
