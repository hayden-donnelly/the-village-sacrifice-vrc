using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ChaseState : BaseState
{
    // The enemy must be within this radius in order to catch the player.
    [SerializeField] private float catchPlayerRadius = 2;

    public override void Construct()
    {
        controller.YellowAlert.SetActive(false);
        controller.RedAlert.SetActive(true);
    }

    public override void Destruct()
    {
        controller.RedAlert.SetActive(false);
    }

    public override void Action()
    {
        if(actionStateIndex == 0)
        {
            // Play animation and scary sound.
            actionStateIndex++;
        }
        if(actionStateIndex == 1)
        {
            Vector3 playerPosition = controller.LP.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
            controller.Agent.SetDestination(playerPosition);

            if(Vector3.Distance(playerPosition, transform.position) < catchPlayerRadius)
            {
                Transform spawn = controller.LocalPlayerSpawn;
                controller.LP.TeleportTo(spawn.position, spawn.rotation);
            }
        }
    }

    public override void Transition()
    {
        if(!controller.PlayerDetected())
        {
            controller.ChangeState(controller.Patrol);
        }
    }
}
