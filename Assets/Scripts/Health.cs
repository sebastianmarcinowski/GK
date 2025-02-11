using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealt = 100;
    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealt;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Damage taken: " + damage);
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth < 0)
        {
            Debug.Log(gameObject.name + " died");
            Destroy(gameObject);
        }
    }
}
