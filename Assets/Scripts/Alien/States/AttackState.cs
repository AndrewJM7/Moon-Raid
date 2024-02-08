using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    private float waitCoooldown = 1f;
    private float attackCooldown = 2f;
    private float currentCooldown = 0f;

    private float moveTimer;
    private float losePlayerTimer;
    private float attackDistance =3f;

    Animator animator;
    NavMeshAgent navMeshAgent;

    public override void Enter()
    {
        animator = alien.GetComponent<Animator>();
        NavMeshAgent navMeshAgent = alien.GetComponent<NavMeshAgent>();
    }

    public override void Exit()
    {
        if (animator != null)
        {
            animator.ResetTrigger("Attack");

        }
    }

    public override void Perform()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && alien.PlayerInSight())
        {
            losePlayerTimer = 0;

            float distanceToPlayer = Vector3.Distance(alien.transform.position, player.transform.position);

            if (distanceToPlayer < attackDistance && currentCooldown <= 0)
            {
                if (navMeshAgent != null)
                {
                    navMeshAgent.isStopped = true;
                }

                WaitCooldown();

                if (animator != null)
                {
                    animator.SetTrigger("Attack");
                }

                player.GetComponent<PlayerHealth>().TakeDamage(10f); 
                currentCooldown = attackCooldown;
            }
            else
            {
                currentCooldown = Mathf.Max(0, currentCooldown - Time.deltaTime);

                alien.Agent.SetDestination(player.transform.position);

                moveTimer += Time.deltaTime;

                if (moveTimer > Random.Range(3, 7))
                {
                    alien.Agent.SetDestination(alien.transform.position + (Random.insideUnitSphere * 5));
                    moveTimer = 0;
                }
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(waitCoooldown);

        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = false;
        }
    }
}

