using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;
using VRC.Udon;

public class EnemyController : UdonSharpBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    [SerializeField] private BaseState patrol;
    private BaseState currentState;
    private VRCPlayerApi localPlayer;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        localPlayer = Networking.LocalPlayer;
        currentState = patrol;
        currentState.Construct();
    }

    private void Update()
    {
        currentState.Action();
        currentState.Transition();
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

    public bool PlayerDetected()
    {
        Vector3 playerHeadPos = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
        if (!Physics.Linecast(transform.position, playerHeadPos))
        {
            return true;
        }
        return false;
    }
}
