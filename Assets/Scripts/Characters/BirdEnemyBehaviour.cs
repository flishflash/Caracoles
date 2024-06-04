using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BirdEnemyBehaviour : MonoBehaviour
{
    bool active = false;

    private NavMeshAgent agent;

    private GameObject player;
    private SnailKnightBehaviour knightScript;

    [SerializeField] private float detectionInterval = 1f; //in seconds
    [SerializeField] private float detectionRadius = 20f;

    public StatsScript enemyStats;
    public AudioSource _attackSFX;


    bool isAttacking = false;

    private ButtonsBehaviour buttonScript;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<StatsScript>();

        buttonScript = GameObject.Find("ButtonManager").GetComponent<ButtonsBehaviour>();
    }

    void Update()
    {
        if (!active)
        {
            if (buttonScript != null)
            {
                if (buttonScript.isSimulating)
                {
                    active = true;
                }
            }
        }
        else
        {
            if (player != null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                    knightScript = player.GetComponent<SnailKnightBehaviour>();
            }
            if (knightScript != null)
            {
                if (!knightScript.inCombat)
                {
                    if (player != null)
                    {
                        InvokeRepeating("FindPlayer", 0f, detectionInterval);
                    }
                }
                else
                {
                    if (player != null)
                    {
                        Fight();
                    }
                }
            }
        }
    }

    void FindPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
        {
            Vector3 direction = player.transform.position - transform.position;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    agent.SetDestination(player.transform.position);
                }
            }
        }
    }

    private void Fight()
    {
        LookAtTarget();

        if (Vector3.Distance(transform.position, player.transform.position) <= 4)
        {
            if (!isAttacking)
            {
                StartCoroutine("DealDamage");
            }
            else
            {
                agent.velocity = Vector3.zero;
            }
        }
    }

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

    IEnumerator DealDamage()
    {
        _attackSFX.Play();
        isAttacking = true;

        if (knightScript.playerStats != null)
        {
            knightScript.playerStats.hp -= enemyStats.dmg;
            Debug.Log("Player hit");
        }
        yield return new WaitForSeconds(enemyStats.spe);

        isAttacking = false;
    }
}