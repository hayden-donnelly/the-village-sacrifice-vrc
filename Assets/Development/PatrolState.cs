using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System.Collections;

public class PatrolState : BaseState
{
    [SerializeField] private Transform[] patrolRoute;
    private int patrolPoint = 0;

    public override void Action()
    {
        // goto is not supported in the current version of udonsharp
        /*switch(actionStateNum)
        {
            case 0:
                controller.agent.SetDestination(patrolRoute[patrolPoint].position);
                actionStateNum++;
                goto case 1;
            case 1:
                if(Vector3.Distance(transform.position, controller.agent.destination) > 2)
                {
                    actionStateNum++;
                }
                goto case 2;
            case 2:
                patrolPoint++;
                if(patrolPoint >= patrolRoute.Length)
                {
                    patrolPoint = 0;
                }
                goto default;
            default:
                actionStateNum = 0;
                break;
        }*/
        if (actionStateNum == 0)
        {
            controller.agent.SetDestination(patrolRoute[patrolPoint].position);
            actionStateNum++;
        }
        if (actionStateNum == 1)
        {
            if (Vector3.Distance(transform.position, controller.agent.destination) < 2)
            {
                actionStateNum++;
            }
            else
            {
                return;
            }
        }
        if (actionStateNum == 2)
        {
            patrolPoint++;
            if (patrolPoint >= patrolRoute.Length)
            {
                patrolPoint = 0;
            }
            actionStateNum = 0;
        }
    }
}
