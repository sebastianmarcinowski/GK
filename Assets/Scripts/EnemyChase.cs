using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float detectionRange = 4f;
    public float attackRange = 1f;
    public float attackCd = 2f;
    private float lastAtack;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            Attack();
            lastAtack = Time.time;
        }
        else
        {
            agent.destination = player.position;
        }
    }

    void Attack()
    {
        Debug.Log("Gracz zosta³ zaatakowany");
    }
}
