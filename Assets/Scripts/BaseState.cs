using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BaseState : UdonSharpBehaviour
{
    public bool unlocked;
    protected int actionStateIndex = 0;
    protected EnemyController controller;

    protected void Start()
    {
        actionStateIndex = 0;
        controller = GetComponent<EnemyController>();
    }

    // This is called whenever a state is transitioned into.
    public virtual void Construct()
    {
        
    }

    // This is called whenever a state is transitioned out of.
    public virtual void Destruct()
    {

    }

    // Main behaviours of a state go here.
    public virtual void Action()
    {

    }

    // Logic for transtioning out of a state goes here.
    public virtual void Transition()
    {

    }
}
