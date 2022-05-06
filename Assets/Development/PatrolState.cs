using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System.Collections;

public class PatrolState : BaseState
{
    [SerializeField] private Transform[] patrolRoute;
    private int patrolPointIndex = 0;
    [SerializeField] private float waitTime = 2;
    private float waitTimeTracker;

    public override void Construct()
    {
        waitTimeTracker = waitTime;
    }

    public override void Destruct()
    {
        controller.Animator.SetInteger("State", 5);
    }

    public override void Action()
    {
        // Set current patrol point.
        if (actionStateIndex == 0)
        {
            controller.agent.SetDestination(patrolRoute[patrolPointIndex].position);
            actionStateIndex++;
        }
        // Wait until agent is close enough to specified patrol point.
        if (actionStateIndex == 1)
        {
            if (Vector3.Distance(transform.position, controller.agent.destination) < 2)
            {
                actionStateIndex++;
            }
            else
            {
                return;
            }
        }
        // Wait for specified length of time at patrol point.
        if (actionStateIndex == 2)
        {
            controller.Animator.SetInteger("State", 4);
            waitTimeTracker -= Time.deltaTime;
            if(waitTimeTracker <= 0)
            {
                controller.Animator.SetInteger("State", 5);
                waitTimeTracker = waitTime;
                actionStateIndex++;
            }
            else
            {
                return;
            }
        }
        // Update patrol point index.
        if (actionStateIndex == 3)
        {
            patrolPointIndex++;
            if (patrolPointIndex >= patrolRoute.Length)
            {
                patrolPointIndex = 0;
            }
            actionStateIndex = 0;
        }
    }

    public override void Transition()
    {
        if(controller.PlayerDetected())
        {
            controller.ChangeState(controller.Chase);
        }
    }
}
