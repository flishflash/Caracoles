using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnailKnightBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    Vector3 target = Vector3.zero;

    public Transform enemy;

    public bool inCombat = false;

    private StatsScript statsScript;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        statsScript = GetComponent<StatsScript>();
        
        agent.SetDestination(target);
    }

    void Update()
    {
        if (inCombat)
        {
            LookAtTarget();
        }
    }

    //Cuando un enemigo está cerca
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter");
        if (other.CompareTag("Enemy"))
        {
            if (!inCombat)
            {
                EnterCombat();
            }
        }
    }

    //Cuando te alejas de un enemigo que tenías cerca
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            inCombat = false;
            agent.SetDestination(target);
        }
    }

    //Iniciar combate
    void EnterCombat()
    {
        inCombat = true;
        agent.SetDestination(enemy.position);
    }

    //Para mirar hacia el enemigo en combate
    void LookAtTarget()
    {
        Vector3 direction = enemy.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10);
        }
    }
}
