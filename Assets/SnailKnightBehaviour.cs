using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnailKnightBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    Vector3 target = Vector3.zero;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
