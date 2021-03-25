using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    float directionHorizontal, platSpeed = 3f;
    bool moveRight = true;

    void Update()
    {
        if (transform.position.x > 4f)
            moveRight = false;
        if (transform.position.x < -4f)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x +
                platSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x -
                platSpeed * Time.deltaTime, transform.position.y);
    }
}
