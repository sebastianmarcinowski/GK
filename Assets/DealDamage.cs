using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int damage = 10;

    void OnCollisionEnter(Collision collision)
    {
        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.takeDamage(damage);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Health targetHealth = other.gameObject.GetComponent<Health>();
        if(targetHealth != null)
        {
            targetHealth.takeDamage(damage);
        }
    }
}
