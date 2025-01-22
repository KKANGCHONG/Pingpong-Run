using UnityEngine;

public class Boundary : MonoBehaviour
{
    public bool isRightSide; // 오른쪽 경계면 여부
    private ScoreManager scoreManager;

    private void Start()
    {
        // ScoreManager 객체 찾기
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ball")) // 공이 충돌했는지 확인
        {
            Debug.Log("Ball hit the boundary!");
            scoreManager.AddScore(isRightSide);
            // 공을 중앙으로 리셋
            ResetBall(other.gameObject);
        }
        else
        {
            Debug.Log($"Unexpected collision with: {other.name}");
        }
    }

    private void ResetBall(GameObject ball)
    {
        ball.transform.position = Vector3.zero;
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        Ball ballScript = ball.GetComponent<Ball>();
        if (ballRb != null && ballScript != null)
        {
            ballRb.linearVelocity = Vector2.zero; // 기존 속도 초기화
            ballRb.angularVelocity = 0f;   // 회전 초기화

            // 초기 속도 설정
            bool isRight = UnityEngine.Random.value >= 0.5f; // 랜덤 방향 결정
            float xVelocity = isRight ? 1f : -1f; // X축 방향 설정
            float yVelocity = UnityEngine.Random.Range(-1f, 1f); // Y축 방향 설정

            // 새로운 속도 적용
            ballRb.linearVelocity = new Vector2(xVelocity, yVelocity).normalized * ballScript.startingSpeed;
        }
    }
}
