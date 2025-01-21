using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Animator animator; // Animator를 드래그하여 연결

    // 애니메이션 이벤트에서 호출할 메서드
    public void ResetState()
    {
        if (animator != null)
        {
            animator.SetInteger("state", 0);
            Debug.Log("State has been reset to 0");
        }
    }
}