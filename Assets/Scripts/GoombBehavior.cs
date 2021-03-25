using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombBehavior : MonoBehaviour
{
    float directionHorizontal, goombSpeed = 3f;
    bool moveRight = true;

    void Update()
    {
        if (transform.position.x > 4f)
            moveRight = false;
        if (transform.position.x < -4f)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x +
                goombSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x -
                goombSpeed * Time.deltaTime, transform.position.y);
    }

    
}
