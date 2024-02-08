using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    // Track which point is being targeted
    public int pointIndex;

    public float waitTimer;

    public override void Enter()
    {

    }

    public override void Perform()
    {
        PatrolCycle();
        if (alien.PlayerInSight())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {

    }

    public void PatrolCycle()
    {
        if (alien.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 1)
            {
                if (pointIndex < alien.path.points.Count - 1)
                {
                    pointIndex++;
                }
                else
                {
                    pointIndex = 0;
                }
                alien.Agent.SetDestination(alien.path.points[pointIndex].position);
                waitTimer = 0;
            }
        }
    }
}

