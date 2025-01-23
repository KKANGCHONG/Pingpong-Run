using UnityEngine;

public class Condition : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) // 플레이어와 충돌 시 실행
        {
            // 알코올 농도를 감소시킴
            GameManager.Instance.Alcohol = Mathf.Clamp(GameManager.Instance.Alcohol - 1, 0, 5);

            // 흔들림 방지 플래그 활성화
            GameManager.Instance.PreventShake = true;

            Debug.Log("Condition triggered, alcohol decreased to: " + GameManager.Instance.Alcohol);

            // Condition 오브젝트 제거
            Destroy(gameObject);
        }
    }
}
