using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;

    public float speed = 3f;
    public float jumpVelocity = 3f;
    
    private float velX;
    private float velY;
    private bool facingRight = true;
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerBoxcollider;
    public Animator animator;
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBoxcollider = transform.GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = playerRigidbody.velocity.y;
        playerRigidbody.velocity = new Vector2(velX * speed, velY);
        animator.SetFloat("Speed", Mathf.Abs(velX));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.velocity = Vector2.up * jumpVelocity;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(playerBoxcollider.bounds.center, playerBoxcollider.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
            this.transform.parent = collision.transform;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
            this.transform.parent = null;
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
        }

        transform.localScale = localScale;
    }
}
