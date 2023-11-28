using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulSpeed = 10f;
    public int bulDamage = 25;
    public float maxDistance = 10f;
    private Rigidbody2D rb;
    private GameObject target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * bulSpeed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided" + collision.name);
        
        
            var damage = collision.GetComponent<PlayerDamage>();
            if (damage != null)
            {
                damage.Hit(bulDamage);
            }

        AudioSource bulImpact = GameObject.FindGameObjectWithTag("bulImpactSource").GetComponent<AudioSource>();
        bulImpact.Play();
        DisableObject();
    }
    private void DisableObject()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
