using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    public bool damageEnemies = false;
    public int dmg;
    public float animationSpeed;

    void Update()
    {
        DoAnimation();
    }

    public virtual void DoAnimation()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<SnailKnightBehaviour>().playerStats.hp -= dmg;
            Debug.Log("Player hit by trap");
        }

        else if (damageEnemies && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<BirdEnemyBehaviour>().enemyStats.hp -= dmg;
            Debug.Log("Enemy hit by trap");
        }
    }
}
