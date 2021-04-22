using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperAirplane : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public Rigidbody2D paperAirplaneRB;

    void Start()
    {
        paperAirplaneRB.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
