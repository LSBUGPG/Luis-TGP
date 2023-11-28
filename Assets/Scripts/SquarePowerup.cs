using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I Trigger");
            PlayerInput Player = other.GetComponent<PlayerInput>();
            if (Player != null)
            {
                Player.SquarePowerupActive();
                Destroy(gameObject);
            }
        }
    }

}
