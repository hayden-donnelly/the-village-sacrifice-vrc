using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;
using VRC.Udon;

public class EnemyController : UdonSharpBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    [SerializeField] private BaseState patrol;
    //private BaseState_ currentState;
    //private BaseState_[] availableStates;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //availableStates = GetComponents<BaseState>();
        //currentState = new Patrol();
        //currentState.Construct();
    }

    private void Update()
    {
        //currentState.Transition();
    }

    /*public void ChangeState(string stateName)
    {
        foreach(BaseState s in availableStates)
        {
            if(s.GetType().FullName != stateName)
            {
                continue;
            }

            if(s.unlocked)
            {
                currentState.Destruct();
                currentState = s;
                currentState.Construct();
            }
            return;
        }
        Debug.LogWarning("New state could not be found.");
    }*/

    private class BaseState_
    {
        public virtual void Construct()
        {
            Debug.Log("Hello World");
        }

        public virtual void Destruct()
        {

        }

        public virtual void Transition()
        {

        }
    }

    private class Patrol : BaseState_
    {

    }

    private BaseState_ currentState;
}
