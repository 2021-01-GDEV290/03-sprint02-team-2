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
        PoggoBehavior poggo = hitInfo.GetComponent<PoggoBehavior>();
        GoombBehavior goomb = hitInfo.GetComponent<GoombBehavior>();

        if(poggo != null)
        {
            poggo.TakeDamage(damage);
        }

        if(goomb != null)
        {
            goomb.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
