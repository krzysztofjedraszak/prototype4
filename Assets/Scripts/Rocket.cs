using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public float rotateSpeed=200;
    
    private Rigidbody rocketRb;
    private Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        rocketRb = GetComponent<Rigidbody>();
        // enemy = GameObject.Find("Enemy")
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Vector3 lookDirection = (enemy.transform.position - transform.position).normalized;
        // //rotate
        //
        // var targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
        // rocketRb.MoveRotation(Quaternion.RotateTowards(transform.rotation,targetRotation,20));
        // rocketRb.AddForce(lookDirection*speed);

        rocketRb.velocity = transform.forward * speed;
        var targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
        rocketRb.MoveRotation(Quaternion.RotateTowards(transform.rotation,targetRotation,rotateSpeed));
        transform.LookAt(enemy);
    }

    void Fire()
    {
        float distance = Mathf.Infinity;
        GameObject[] tab = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var go in tab)
        {
            float diff = (go.transform.position - transform.position).sqrMagnitude;
            if (diff<distance)
            {
                distance = diff;
                enemy = go.transform;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
