using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    public bool damageEnemies = false;
    public int dmg;
    public float animationSpeed;

    private bool landed = false;

    private void Start()
    {
        StartCoroutine(StartAnimation());
    }

    void Update()
    {
        if (landed) DoAnimation();
    }

    /// <summary>
    /// Before landing
    /// </summary>
    public IEnumerator StartAnimation()
    {
        Vector3 initialScale = transform.localScale;
        float initialHeight = transform.localPosition.y;

        transform.localPosition = transform.localPosition + new Vector3(0, 10, 0);

        float time = 0;
        float landingTime = 0.25f;

        while (time <= landingTime) 
        {
            float currentHeight = Mathf.Lerp(initialHeight + 10, initialHeight, time * (1 / landingTime));
            transform.localPosition = new Vector3(transform.localPosition.x, currentHeight, transform.localPosition.z);

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, initialHeight, transform.localPosition.z);
        landed = true;
    }

    /// <summary>
    /// After landing
    /// </summary>
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
