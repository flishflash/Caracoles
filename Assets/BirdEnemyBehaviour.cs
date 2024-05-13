using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BirdEnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;

    public GameObject player;
    private SnailKnightBehaviour knightScript;

    [SerializeField] private float detectionInterval = 1f; //in seconds
    [SerializeField] private float detectionRadius = 10f;

    private StatsScript statsScript;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        statsScript = GetComponent<StatsScript>();

        knightScript = player.GetComponent<SnailKnightBehaviour>();

        InvokeRepeating("FindPlayer", 0f, detectionInterval);
    }

    void Update()
    {
        if (knightScript.inCombat)
        {
            LookAtTarget();
        }
    }

    void FindPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    //private void OnTriggerStay(Collision collision)
    //{
    //    //Debug.Log("Colisión detectada");
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        agent.velocity = Vector3.zero;
    //        Debug.Log("Stop");
    //    }
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        //agent.velocity = Vector3.zero;
    //    }
    //}

    //Para mirar hacia el enemigo en combate
    void LookAtTarget()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10);
        }
    }
}
