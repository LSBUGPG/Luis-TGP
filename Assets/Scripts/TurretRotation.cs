using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretRotation : MonoBehaviour
{
    private CustomInput inputs;
    public Transform target;   
    public float rotationSpeed = 5.0f;

    public void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void FindNewTarget()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyObjects.Length > 0)
        {
            // Choose a random enemy object from the list
            int randomIndex = Random.Range(0, enemyObjects.Length);
            target = enemyObjects[randomIndex].transform;

        }
    }

    private void Update()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (target == null)
        {
            FindNewTarget();

        }
    }
}
