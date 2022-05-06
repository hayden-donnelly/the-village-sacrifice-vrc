using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;
using VRC.Udon;

public class EnemyController : UdonSharpBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    public BaseState Patrol;
    public BaseState Chase;
    [SerializeField] private BaseState currentState;
    public VRCPlayerApi LocalPlayer;
    // Where the player will be sent to if they are caught by the enemy.
    public Transform LocalPlayerSpawn;
    public Animator Animator;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        LocalPlayer = Networking.LocalPlayer;
        currentState = Patrol;
        currentState.Construct();
    }

    private void Update()
    {
        currentState.Action();
        currentState.Transition();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Destruct();
        currentState = newState;
        currentState.Construct();
    }

    public bool PlayerDetected()
    {
        Vector3 playerHeadPos = LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
        if (!Physics.Linecast(transform.position, playerHeadPos) 
            && Vector3.Distance(transform.position, playerHeadPos) <= 8)
        {
            return true;
        }
        return false;
    }
}
