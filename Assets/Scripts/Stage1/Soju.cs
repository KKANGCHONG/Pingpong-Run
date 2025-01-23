using UnityEngine;

public class Soju : MonoBehaviour
{
    private Animator animator;
    private ShakeEffect shakeEffect;
    private int lastAlcoholLevel = 0; // 이전 알코올 값 저장

    void Start()
    {
        // Animator 컴포넌트를 가져옴
        animator = GetComponent<Animator>();

        // ShakeEffect 스크립트를 가진 오브젝트를 찾음
        shakeEffect = Object.FindAnyObjectByType<ShakeEffect>();
    }

    void Update()
    {
        // 현재 Alcohol 값을 가져옴
        int alcohol = GameManager.Instance.Alcohol;

        // Alcohol 값을 Animator에 전달
        animator.SetInteger("Alcohol", alcohol);

        // 알코올 값이 변경되었을 때만 흔들림 효과 실행
        if (alcohol != lastAlcoholLevel && shakeEffect != null)
        {
            shakeEffect.TriggerShake(alcohol); // 알코올 값 기반으로 흔들림 실행
            lastAlcoholLevel = alcohol; // 이전 알코올 값 업데이트
        }
    }
}
