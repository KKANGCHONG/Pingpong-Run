using UnityEngine;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }
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

    void StartInvincible()  {
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    void StopInvincible()  {
        isInvincible = false;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "ground") {
            if(!isGrounded) {
                PlayerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)  {
        if(collider.gameObject.tag == "enemy")  {
            if(!isInvincible){
                Destroy(collider.gameObject);
                Hit();
            }
        }
        else if(collider.gameObject.tag == "food")  {
            Destroy(collider.gameObject);
            Heal();
        }
        else if(collider.gameObject.tag == "golden")    {
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}
