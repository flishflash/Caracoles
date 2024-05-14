using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnailKnightBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    Vector3 target = Vector3.zero;

    [HideInInspector] public bool inCombat = false;
    bool isAttacking = false;

    [HideInInspector] public StatsScript playerStats;

    private GameObject enemy;
    private StatsScript enemyStats;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerStats = GetComponent<StatsScript>();
        
        agent.SetDestination(target);
    }

    void Update()
    {
        if (inCombat)
        {
            if (enemy != null)
            {
                Fight();
            }
        }
        else
        {
            agent.SetDestination(target);
        }
    }

    //Cuando un enemigo está cerca
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!inCombat)
            {
                enemy = other.gameObject;
                enemyStats = enemy.GetComponent<StatsScript>();

                Vector3 direction = enemy.transform.position - transform.position;

                RaycastHit hit;

                if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        EnterCombat();
                    }
                }
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
        agent.SetDestination(enemy.transform.position);
    }

    void ExitCombat()
    {
        inCombat = false;
        isAttacking = false;
        agent.SetDestination(target);
    }

    private void Fight()
    {
        LookAtTarget();

        if (Vector3.Distance(transform.position, enemy.transform.position) <= 4)
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
        Vector3 direction = enemy.transform.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10);
        }
    }

    IEnumerator DealDamage()
    {
        isAttacking = true;

        if (enemyStats != null)
        {
            enemyStats.hp -= playerStats.dmg;
            Debug.Log("Enemy hit");
            if (enemyStats.hp <= 0)
            {
                ExitCombat();
            }
        }
        yield return new WaitForSeconds(playerStats.spe);
        
        isAttacking = false;
    }
}
