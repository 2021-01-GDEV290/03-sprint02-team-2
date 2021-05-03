using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoggoBehavior : MonoBehaviour
{
    public int health = 1;
    public GameObject poggo;

    [Header("For Patrolling")]
    public float moveSpeed = 1f;
    private float moveDirection = 1;
    private bool facingRight = true;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    private bool checkingGround;
    private bool checkingWall;

    [Header("For JumpingAttack")]
    [SerializeField] int jumpHeight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    private bool isGrounded;

    [Header("For SeeingPlayer")]
    [SerializeField] Vector2 lineOfSight;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;

    [Header("Other")]
    private Animator poggoAnimator;
    private Rigidbody2D poggoRB;

    void Start()
    {
        poggoRB = GetComponent<Rigidbody2D>();
        poggoAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSight, 0, playerLayer);
        AnimationController();
        if (!canSeePlayer && isGrounded)
        {
            Patrolling();
        }
        
    }

    void Update()
    {
        if (poggo.transform.position.y < -2)
        {
            Object.Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            poggoAnimator.Play("Poggo_Death");
            Object.Destroy(gameObject, 1f);
        }
    }

    void Patrolling()
    {
        if ((!checkingGround || checkingWall))
        {
            Flip();
        }

        else if (!facingRight && checkingWall)
        {
            Flip();
        }

        poggoRB.AddForce(new Vector2(moveSpeed * moveDirection, jumpHeight), ForceMode2D.Impulse);
    }

    void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

        if (isGrounded)
        {
            poggoRB.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
        }
    }

    void FlipTowardsPlayer()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

        if (distanceFromPlayer < 0 && facingRight)
        {
            Flip();
        }

        else if(distanceFromPlayer>0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void AnimationController()
    {
        poggoAnimator.SetBool("canSeePlayer", canSeePlayer);
        poggoAnimator.SetBool("isGrounded", isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSight);
    }
}
