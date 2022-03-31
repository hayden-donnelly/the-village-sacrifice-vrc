using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BaseState : UdonSharpBehaviour
{
    public bool unlocked;
    protected UdonSharpBehaviour controller;

    protected void Start()
    {
        controller = GetComponent<EnemyController>();
    }

    public virtual void Construct()
    {

    }

    public virtual void Destruct()
    {

    }

    public virtual void Transition()
    {

    }
}
