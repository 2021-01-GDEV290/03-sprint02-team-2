using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject paperAirplane;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(paperAirplane, throwPoint.position, throwPoint.rotation);
    }
}
