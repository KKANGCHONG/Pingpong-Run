using UnityEngine;

public class Jinwoo : MonoBehaviour
{
    [Header("Settings")]
    public GameObject pingPongBallPrefab; // 탁구공 프리팹
    public Transform throwPoint; // 공을 던질 위치
    public float ballThrowForce = 5f; // 공 던지는 힘
    public float minThrowInterval = 3f; // 최소 시간 간격
    public float maxThrowInterval = 5f; // 최대 시간 간격

    [Header("References")]
    public Animator playerAnimator;

    private bool isThrowing = false;

    void Start()
    {
        // 시작 시 공 던지는 주기 설정
        StartThrowing();
    }

    void StartThrowing()
    {
        // 3~5초 사이의 랜덤한 시간 후 ThrowBall 동작 실행
        float throwInterval = Random.Range(minThrowInterval, maxThrowInterval);
        Invoke(nameof(TriggerThrowAnimation), throwInterval);
    }

    void TriggerThrowAnimation()
    {
        if (isThrowing) return;

        isThrowing = true;

        // 던지는 애니메이션으로 전환
        playerAnimator.SetTrigger("throw");

        // 공 던지기 실행
        Invoke(nameof(ThrowBall), 0.5f); // 던지는 동작 중간에 공 던지기
    }

    void ThrowBall()
    {
        if (pingPongBallPrefab != null && throwPoint != null)
        {
            // 공 생성
            GameObject ball = Instantiate(pingPongBallPrefab, throwPoint.position, Quaternion.identity);

            // 공에 물리적 힘 추가
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                ballRb.linearVelocity = transform.localScale.x * Vector2.right * ballThrowForce; // Jinwoo의 방향에 따라 공 던지기
            }
        }

        // 다시 달리는 상태로 복귀
        Invoke(nameof(ResetToRun), 0.5f);
    }

    void ResetToRun()
    {
        playerAnimator.SetTrigger("run");
        isThrowing = false;

        // 다음 공 던지기 예약
        StartThrowing();
    }
}
