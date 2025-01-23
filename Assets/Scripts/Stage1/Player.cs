using UnityEngine;
using System.Collections;

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

    void Start()
    {
        
    }

    void Update()
    {
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }

        // 슬라이드 
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded)
        {
            PlayerAnimator.SetInteger("state", 3);
        }

        // 슬라이드 유지 (KeyCode.DownArrow를 누르고 있는 동안)
        if (Input.GetKey(KeyCode.DownArrow) && isGrounded)
        {
            PlayerAnimator.SetInteger("state", 3); // 계속 슬라이드 유지
        }

        // 슬라이드 종료 (KeyCode.DownArrow를 뗐을 때)
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            PlayerAnimator.SetInteger("state", 4); // 슬라이드 종료 상태
        }
    }

    public void KillPlayer()
    {
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    void Hit()
    {
        GameManager.Instance.Lives -= 1;
        if (GameManager.Instance.Lives == 0)
        {
            KillPlayer();
        }
    }
    void Spin()
    {
        GameManager.Instance.Lives -= 1;
        if (GameManager.Instance.Lives == 0)
        {
            KillPlayer();
        }
    }

    void Heal()
    {
        GameManager.Instance.Lives = Mathf.Min(3, GameManager.Instance.Lives + 1);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Bottom")
        {
            if (!isGrounded)
            {
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        // 소주와 충돌 시 알코올 증가
        if (collider.CompareTag("Soju"))
        {
            Destroy(collider.gameObject); // 소주는 사라짐
            GameManager.Instance.Alcohol = Mathf.Clamp(GameManager.Instance.Alcohol + 1, 0, 5);
        }

        // 컨디션과 충돌 시 알코올 감소
        else if (collider.CompareTag("Condition"))
        {
            Destroy(collider.gameObject); // 컨디션은 사라짐
            GameManager.Instance.Alcohol = Mathf.Clamp(GameManager.Instance.Alcohol - 1, 0, 5);
        }

        else if (collider.CompareTag("enemy") || collider.CompareTag("ball"))
        {
            if (!isInvincible)
            {
                Destroy(collider.gameObject);
                Hit();
            }
        }
        else if (collider.CompareTag("food"))
        {
            Destroy(collider.gameObject);
            Heal();
        }

        else if (collider.CompareTag("Error") && isHit == false)
        {
            isHit = true;
            PlayerAnimator.SetInteger("state", 5); // Jinwoo Hit 상태로 변경
            Hit();
            StartCoroutine(ResetToRun());
        }

        else if (collider.CompareTag("Commit") && isHit == false)
        {
            isHit = true;
            PlayerAnimator.SetInteger("state", 5); // Jinwoo Hit 상태로 변경
            Hit();
            StartCoroutine(ResetToRun());
        }

        else if (collider.CompareTag("Madcamp") && isHit == false)
        {
            isHit = true;
            PlayerAnimator.SetInteger("state", 5); // Jinwoo Hit 상태로 변경
            Hit();
            Destroy(collider.gameObject); // 블록 없애기
            StartCoroutine(ResetToRun());
        }

        else if (collider.CompareTag("Banana") && isHit == false)
        {
            isHit = true;
            PlayerAnimator.SetInteger("state", 6); // Jinwoo Spin 상태로 변경
            Spin(); // Spin()으로 바꾸기
            Destroy(collider.gameObject); // 블록 없애기
            StartCoroutine(ResetToRun());
        }

        else if (collider.gameObject.tag.StartsWith("Ball"))
        {
            Destroy(collider.gameObject);
            get_Ball(collider.gameObject.tag);
        }
    }

    void get_Ball(string ballTag)
    {
        switch (ballTag)
        {
            case "Ball_1":
                GameManager.Instance.BallNumber += 1;
                break;
            case "Ball_2":
                GameManager.Instance.BallNumber += 2;
                break;
            case "Ball_3":
                GameManager.Instance.BallNumber += 3;
                break;
            case "Ball_4":
                GameManager.Instance.BallNumber += 4;
                break;
            case "Ball_5":
                GameManager.Instance.BallNumber += 5;
                break;
            default:
                Debug.LogWarning("Unknown ball tag: " + ballTag);
                break;
        }

        // UI 업데이트
        if (GameManager.Instance.BallNumberText != null)
        {
            GameManager.Instance.BallNumberText.text = "Ball: " + GameManager.Instance.BallNumber.ToString();
        }
    }
}
