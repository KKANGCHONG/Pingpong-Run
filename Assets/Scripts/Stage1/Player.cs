using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidBody;
    public BoxCollider2D PlayerCollider;
    public Animator PlayerAnimator;
    private bool isGrounded = true;
    private bool isInvincible = false;
    public float moveSpeed = 2f;   // 이동 속도
    private bool isHit = false; // 충돌 상태 플래그
    // private bool isMovingToStair = false; // 계단으로 이동 중인지 확인
    // private Vector3 stairTarget; // 계단 위치

    void Start()
    {
        
    }

    void Update()
    {
        // 점프
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }
        // 슬라이드 
        if(Input.GetKeyDown(KeyCode.DownArrow) && isGrounded ) 
        {
            PlayerAnimator.SetInteger("state", 3);
        }

        // 슬라이드 유지 (KeyCode.DownArrow를 누르고 있는 동안)
        if(Input.GetKey(KeyCode.DownArrow) && isGrounded) 
        {
            PlayerAnimator.SetInteger("state", 3); // 계속 슬라이드 유지
        }

        // 슬라이드 종료 (KeyCode.DownArrow를 뗐을 때)
        if(Input.GetKeyUp(KeyCode.DownArrow)) 
        {
            PlayerAnimator.SetInteger("state", 4); // 슬라이드 종료 상태
        }

        // //계단 오르는 거
        // if (isMovingToStair)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, stairTarget, moveSpeed * Time.deltaTime);

        //     // 목표 위치에 도달했는지 확인
        //     if (Vector3.Distance(transform.position, stairTarget) < 0.1f)
        //     {
        //         isMovingToStair = false;
        //         Debug.Log("Player reached the stair.");
        //         StartClimbing(); // 계단 오르기 시작
        //     }
        // }

    }

    public void KillPlayer()   {
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    void Hit()  {
        GameManager.Instance.Lives -= 1;
        if(GameManager.Instance.Lives == 0)  {
            KillPlayer();
        }
    }

    void Heal()  {
        GameManager.Instance.Lives = Mathf.Min(3, GameManager.Instance.Lives + 1);
    }

    // 소주 마셔서 알코올 농도 높아짐 - 테스트 
    void drink_soju()  {
        GameManager.Instance.Alcohol += 1;
        if(GameManager.Instance.Alcohol == 5)  {
            // 화면 흔들리는걸로 로직 수정하기
            Debug.Log("Alcohol maxed out! Player is now affected."); // 디버그용
        }
    }

    // 컨디션 마셔서 농도 낮아짐
    void drink_condition() {
        GameManager.Instance.Alcohol = Mathf.Min(5, GameManager.Instance.Alcohol-1);
    }


    void OnCollisionEnter2D(Collision2D collision2D) {
        if(collision2D.gameObject.tag == "Bottom") {
            if(!isGrounded) {
                PlayerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;
        }
    }

    IEnumerator ResetToRun()
{
    // Jinwoo Hit 애니메이션의 길이를 가져옴
    float animationLength = PlayerAnimator.GetCurrentAnimatorStateInfo(0).length;

    yield return new WaitForSeconds(animationLength);

    PlayerAnimator.SetInteger("state", 0); // Jinwoo Run 상태로 변경
    isHit = false; // 충돌 상태 초기화
}

    void OnTriggerEnter2D(Collider2D collider)  {
        if(collider.gameObject.tag == "enemy" || collider.gameObject.tag == "ball")  {
            if(!isInvincible){
                Destroy(collider.gameObject);
                Hit();
            }
        }
        else if(collider.gameObject.tag == "food")  {
            Destroy(collider.gameObject);
            Heal();
        }

        //소주 마시면 알코올 농도 오르는거 구현 
        else if(collider.gameObject.tag == "Soju")  {
            Destroy(collider.gameObject); // 소주는 사라짐 
            drink_soju();
            Heal();
        }

        else if(collider.gameObject.tag == "Condition")  {
            Destroy(collider.gameObject); // 컨디션  사라짐 
            drink_condition();
        }

        else if (collider.gameObject.tag == "Error" && isHit == false)
        {
            isHit = true; 
            PlayerAnimator.SetInteger("state", 5); // Jinwoo Hit 상태로 변경
            Hit();

            StartCoroutine(ResetToRun());
        }

        else if (collider.gameObject.tag == "Commit" && isHit == false)
        {
            isHit = true; 
            PlayerAnimator.SetInteger("state", 5); // Jinwoo Hit 상태로 변경
            Hit();

            StartCoroutine(ResetToRun());
        }

        else if (collider.gameObject.tag == "Madcamp" && isHit == false)
        {
            isHit = true; 
            PlayerAnimator.SetInteger("state", 5); // Jinwoo Hit 상태로 변경
            Hit();
            Destroy(collider.gameObject); // 블록 없애기

            StartCoroutine(ResetToRun());
        }
    }

    // public void StartMovingToStair(Vector3 targetPosition)
    // {
    //     isMovingToStair = true;
    //     stairTarget = new Vector3(targetPosition.x, transform.position.y, transform.position.z); // X축 위치만 맞춤
    //     Debug.Log("Player is moving to the stair.");
    // }

    // private void StartClimbing()
    // {
    //     Debug.Log("Player starts climbing the stair.");
    //     // 계단 오르기 애니메이션 또는 추가 로직
    // }
}