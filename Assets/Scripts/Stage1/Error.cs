using UnityEngine;
using System.Collections;

public class Error : MonoBehaviour
{
    [Header("References")]
    public BoxCollider2D ErrorCollider;
    public Animator ErrorAnimator;

    private bool isHit = false; // 충돌 상태 플래그
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ResetToError()
    {
        // Jinwoo Hit 애니메이션의 길이를 가져옴
        float animationLength = ErrorAnimator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);

        ErrorAnimator.SetInteger("error", 0); 
        isHit = false; 
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isHit = true;
            ErrorAnimator.SetInteger("error", 1); // Jinwoo Hit 상태로 변경
            StartCoroutine(ResetToError());
        }

    }
}
