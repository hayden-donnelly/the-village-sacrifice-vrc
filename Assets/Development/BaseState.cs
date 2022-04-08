using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BaseState : UdonSharpBehaviour
{
    public bool unlocked;
    [HideInInspector] public int actionStateNum = 0;
    protected EnemyController controller;

    protected void Start()
    {
        actionStateNum = 0;
        controller = GetComponent<EnemyController>();
    }

    public virtual void Construct()
    {

    }

    public virtual void Destruct()
    {

    }

    public virtual void Action()
    {

    }

    public virtual void Transition()
    {

    }
}
