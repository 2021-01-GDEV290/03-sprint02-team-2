using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.TriggerPickupInvincibility();
            gameObject.SetActive(false);
        }
    }
}
