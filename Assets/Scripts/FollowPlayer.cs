using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    public float shootingRange;
    public GameObject bullet;
    public GameObject bulletParent;
    public Transform target;

    public float fireRate = 1f;
    private float reloadRate;


    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void FindNewTarget()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyObjects.Length > 0)
        {
        
            int randomIndex = Random.Range(0, enemyObjects.Length);
            target = enemyObjects[randomIndex].transform;

        }
    }

    public AudioSource bulSound;
    void Update()
    {
        
        float distanceFromPlayer = Vector2.Distance(target.position, transform.position);
       if (distanceFromPlayer <= shootingRange && reloadRate < Time.time)
        {
            Debug.Log("ShootingEnemy");
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            reloadRate = Time.time + fireRate;
            bulSound.Play();
        }

       if (target == null) 
        {
            FindNewTarget();

        }
    }

}
