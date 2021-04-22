using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;

    public float speed = 3f;
    public float jumpVelocity = 3f;
    public int playerHealth = 4;
    public GameObject ThrowPoint;
    
    private float velX;
    private float velY;
    private bool facingRight = true;
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerBoxcollider;
    private Animator animator;
    
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

        else if (Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.velocity = Vector2.up * jumpVelocity;
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Jump", 1);
            animator.SetFloat("Idle", 0);
        }

        else if(IsGrounded())
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Jump", 0);
            animator.SetFloat("Idle", 1);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(playerBoxcollider.bounds.center, playerBoxcollider.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        //animator.SetFloat("Jump", 0);
        return raycastHit2d.collider != null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
            this.transform.parent = collision.transform;
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

    public void Damage()
    {
        playerHealth--;
        if(playerHealth > 0)
        {
            animator.Play("Player Hurt");
        }
        else
        {
            animator.Play("Player Death");
            Object.Destroy(gameObject, 1f);
        }
    }

    void LateUpdate()
    {
        Vector3 localScale = transform.localScale;
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
