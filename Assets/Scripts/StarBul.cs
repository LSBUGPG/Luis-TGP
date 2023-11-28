using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBul : MonoBehaviour
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

        var damage = collision.GetComponent<Damage>();
        if (damage != null)
        {
            damage.Hit(bulDamage);
        }

    }
}
