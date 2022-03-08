using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    private NavMeshAgent agent;
    public AudioSource audioSource1;

    //Updates the players position every seconds for the zombies. If the destination is less then 2.5, the zombie begin the attack animation. 

    private void Update()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent.isActiveAndEnabled == false)
        {
            return;
        }
        target = PlayerManager.instance.player.transform;
        agent.SetDestination(target.position);
        float attackRange = Vector3.Distance(agent.transform.position, target.transform.position);

        if(attackRange < 5)
        {
            audioSource1.Play();
        }
        if (attackRange < 2.5)
        {
            agent.isStopped = true;
            Enemy enemy = GetComponent<Enemy>();
            enemy.Attack();
            StartCoroutine(WaitingTime());
        }       
    }
    IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(3);
        agent.isStopped = false;
    }
}