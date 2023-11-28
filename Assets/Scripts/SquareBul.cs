using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SquareBul : MonoBehaviour
{
    public float bulSpeed = 10f;
    public int bulDamage = 25;
    public float maxDistance = 10f;




    private Vector2 startPos;
    private float totalDistance = 0f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        startPos = transform.position;
        rb.velocity = transform.up * bulSpeed;

    }

    public void Update()
    {
 
        totalDistance = Vector2.Distance(transform.position, startPos);
        if (totalDistance >= maxDistance) 
        {
            DisableObject();
        
        }


    }

    private void DisableObject()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided"+collision.name);

        var target = collision.GetComponent<AIDestinationSetter>();
        var damage = collision.GetComponent<FollowPlayer>();
        var aim = collision.GetComponent<TurretRotation>();

        if (damage != null)
        {
            target.tarSwitch();
            damage.FindNewTarget();
            aim.FindNewTarget();
            
        }
        DisableObject();
    }
}
