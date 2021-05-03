using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    public float speed = 3f;
    public float jumpVelocity = 3f;
    public int playerHealth = 4;
    public int maxHealth = 4;
    public GameObject ThrowPoint;
    public GameObject player;

    public Image heart;
    public Sprite fullHeart;
    public Sprite hitOneHeart;
    public Sprite hitTwoHeart;
    public Sprite hitThreeHeart;

    private float velX;
    private float velY;
    private bool facingRight = true;
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerBoxcollider;
    private Animator animator;

    private bool isInvincible = false;

    [SerializeField] private float invincibilityDurationSeconds;
    [SerializeField] private float invincibilityDeltaTime;
    [SerializeField] private float pickupInvincibilityDurationSeconds;
    [SerializeField] private float pickupInvincibilityDeltaTime;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBoxcollider = transform.GetComponent<BoxCollider2D>();
        animator = this.GetComponent<Animator>();
    }


    void Update()
    {
        velX = Input.GetAxis("Horizontal");
        velY = playerRigidbody.velocity.y;
        playerRigidbody.velocity = new Vector2(velX * speed, velY);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetFloat("Speed", 1);
            animator.SetFloat("Jump", 0);
            animator.SetFloat("Idle", 0);
        }

        else if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            playerRigidbody.velocity = Vector2.up * jumpVelocity;
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Jump", 1);
            animator.SetFloat("Idle", 0);
        }

        else if (IsGrounded())
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Jump", 0);
            animator.SetFloat("Idle", 1);
        }

        if (playerHealth == 4)
        {
            heart.sprite = fullHeart;
        }

        else if(playerHealth == 3)
        {
            heart.sprite = hitOneHeart;
        }

        else if (playerHealth == 2)
        {
            heart.sprite = hitTwoHeart;
        }

        else if (playerHealth == 1)
        {
            heart.sprite = hitThreeHeart;
        }

        else if(playerHealth == 0)
        {
            heart.enabled = false;
        }

        if (player.transform.position.y < -2)
        {
            animator.Play("Player Death");
            Object.Destroy(gameObject, 1f);
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(playerBoxcollider.bounds.center, playerBoxcollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        return raycastHit2d.collider != null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = collision.transform;
        }
        if (collision.gameObject.name.Equals("Goomb"))
            Damage();
        if (collision.gameObject.name.Equals("Poggo"))
            Damage();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
            this.transform.parent = null;
    }

    public void Heal(int amount)
    {
        playerHealth += amount;
        playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);
    }

    public void Damage()
    {
        if (isInvincible) return;
        playerHealth--;

        if(playerHealth > 0)
        {
            animator.Play("Player Hurt");
            StartCoroutine(Invinciblity());
        }
        else
        {
            animator.Play("Player Death");
            Object.Destroy(gameObject, 1f);
        }
    }

    public void TriggerPickupInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(PickupInvinciblity());
            
        }
    }

    private IEnumerator Invinciblity()
    {
        isInvincible = true;

        for (float i = 0; i<invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        isInvincible = false;
    }

    private IEnumerator PickupInvinciblity()
    {
        isInvincible = true;

        for (float i = 0; i < pickupInvincibilityDurationSeconds; i += pickupInvincibilityDeltaTime)
        {
            yield return new WaitForSeconds(pickupInvincibilityDeltaTime);
        }
        isInvincible = false;
    }

    void LateUpdate()
    {
        Vector2 localScale = transform.localScale;
        if (velX > 0)
        {
            facingRight = true;
        }

        else if (velX < 0)
        {
            facingRight = false;
        }

        if(((facingRight) && (localScale.x<0))|| ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
            ThrowPoint.transform.Rotate(0f, 180f, 0f);
        }

        transform.localScale = localScale;
    }
}
