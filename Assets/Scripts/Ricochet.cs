using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    public float speed = 10f;
    public int maxRicochets = 3; 
    private int ricochetCount = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AudioSource bulImpact = GameObject.FindGameObjectWithTag("bulImpactSource").GetComponent<AudioSource>();
            bulImpact.Play();
            Debug.Log("Enemy Hit");
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Barrier"))
        {
            if (ricochetCount < maxRicochets)
            {
                AudioSource bulImpact = GameObject.FindGameObjectWithTag("bulImpactSource").GetComponent<AudioSource>();
                bulImpact.Play();
                Vector2 normal = collision.ClosestPoint(transform.position) - (Vector2)transform.position;
                GetComponent<Rigidbody2D>().velocity = Vector2.Reflect(GetComponent<Rigidbody2D>().velocity, normal.normalized);
                ricochetCount++;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
