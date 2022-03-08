using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    [Header("Enemy Stats")]
    public int maxHealth = 100;
    float currentHealth;
    public float enemyDamage = 10f;
    public bool isdead = false;

    public LayerMask playerLayer;
    public GameObject character;

    public GameObject coins;

    public static int grudgecount = 0;

    // Sets the health of the enemies to the starting health.
    void Start()
    {
        currentHealth = maxHealth;
    }
    // The enemy shot takes damagge equals to the variable. Sent from Musket.
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {         
            Die();
        }
    }

    // Enemy attack
    public void Attack()
    {
        StartCoroutine(WaitingTime(1));
        animator.SetBool("Attacking", true);     
    }

    // De-activates the zombie model and its components.

    void Die()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        animator.SetBool("IsDead", true);
        isdead = true;
        grudgecount++;
        GameObject gold = Instantiate(coins, transform.position, Quaternion.Euler(0, 180, 0));
        StartCoroutine(WaitingTime(2));
    }
    IEnumerator WaitingTime(int time)
    {
        switch (time)
        {
            case 1:
                yield return new WaitForSeconds(3);
                animator.SetBool("Attacking", false);
                break;
            case 2:
                yield return new WaitForSeconds(3);
                Destroy(gameObject);
                break;
        }
    }
}
