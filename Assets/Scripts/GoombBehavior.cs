using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombBehavior : MonoBehaviour
{
    public int health = 1;
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;
    bool moveRight = true;
    private Animator goombAnimator;
    private Rigidbody2D goombRB;

    Vector3 nextPos;

    void Start()
    {
        goombAnimator = GetComponent<Animator>();
        nextPos = startPos.position;
    }

    void Update()
    {
        goombAnimator.SetFloat("Walking", 1);

        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }

        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            goombAnimator.Play("Goomb_Die");
            Object.Destroy(gameObject, 1f);
        }
    }

    void LateUpdate()
    {
        Vector3 localScale = transform.localScale;

        if (((moveRight) && (localScale.x < 0)) || ((!moveRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
