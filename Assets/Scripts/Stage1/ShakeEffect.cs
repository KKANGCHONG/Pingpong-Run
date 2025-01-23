// using UnityEngine;

// public class ShakeEffect : MonoBehaviour
// {
//     public Transform cameraTransform; // 카메라 Transform
//     public float baseShakeDuration = 0.5f; // 기본 흔들림 지속 시간
//     public float baseShakeMagnitude = 0.05f; // 기본 흔들림 세기

//     private Vector3 originalPosition; // 카메라 원래 위치
//     private float remainingShakeTime; // 남은 흔들림 시간
//     private float shakeMagnitude; // 현재 흔들림 세기

//     void Start()
//     {
//         if (cameraTransform == null)
//         {
//             cameraTransform = Camera.main.transform; // 메인 카메라 설정
//         }
//         originalPosition = cameraTransform.localPosition;
//     }

//     void Update()
//     {
//         if (remainingShakeTime > 0)
//         {
//             // 카메라를 무작위로 흔들기
//             cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

//             // 흔들림 시간 감소
//             remainingShakeTime -= Time.deltaTime;

//             if (remainingShakeTime <= 0)
//             {
//                 cameraTransform.localPosition = originalPosition; // 원래 위치로 복구
//             }
//         }
//     }

//     public void TriggerShake(int alcoholLevel)
//     {
//         // 흔들림 세기와 지속 시간은 Alcohol 레벨에 비례
//         shakeMagnitude = baseShakeMagnitude * alcoholLevel; // 레벨이 클수록 세게 흔들림
//         remainingShakeTime = baseShakeDuration + (alcoholLevel * 0.1f); // 레벨이 클수록 오래 흔들림
//     }
// }
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public Transform cameraTransform; // 카메라 Transform
    public float baseShakeDuration = 0.5f; // 기본 흔들림 지속 시간
    public float baseShakeMagnitude = 0.05f; // 기본 흔들림 세기

    private Vector3 originalPosition; // 카메라 원래 위치
    private float remainingShakeTime; // 남은 흔들림 시간
    private float shakeMagnitude; // 현재 흔들림 세기

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // 메인 카메라 설정
        }
        originalPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        // 흔들림 방지 플래그가 활성화된 경우 흔들림을 실행하지 않음
        if (GameManager.Instance.PreventShake)
        {
            // 흔들림 방지 플래그가 활성화되었으므로 초기화
            remainingShakeTime = 0;
            cameraTransform.localPosition = originalPosition; // 카메라 위치 복구
            GameManager.Instance.PreventShake = false; // 플래그 초기화
            return; // 흔들림 방지
        }

        if (remainingShakeTime > 0)
        {
            // 카메라를 무작위로 흔들기
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            // 흔들림 시간 감소
            remainingShakeTime -= Time.deltaTime;

            if (remainingShakeTime <= 0)
            {
                cameraTransform.localPosition = originalPosition; // 원래 위치로 복구
            }
        }
    }

    public void TriggerShake(int alcoholLevel)
    {
        // 흔들림 방지 플래그가 활성화된 경우 흔들림을 실행하지 않음
        if (GameManager.Instance.PreventShake)
        {
            return; // 흔들림 실행 취소
        }

        // 흔들림 세기와 지속 시간은 알코올 레벨에 비례
        shakeMagnitude = baseShakeMagnitude * alcoholLevel; // 레벨이 클수록 세게 흔들림
        remainingShakeTime = baseShakeDuration + (alcoholLevel * 0.1f); // 레벨이 클수록 오래 흔들림
    }
}
