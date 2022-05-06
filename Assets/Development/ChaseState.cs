using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ChaseState : BaseState
{
    // The enemy must be within this radius in order to catch the player.
    [SerializeField] private float catchPlayerRadius = 2;

    public override void Action()
    {
        if(actionStateIndex == 0)
        {
            // Play animation and scary sound.
            actionStateIndex++;
        }
        if(actionStateIndex == 1)
        {
            Vector3 playerPosition = controller.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
            controller.agent.SetDestination(playerPosition);

            if(Vector3.Distance(playerPosition, transform.position) < catchPlayerRadius)
            {
                Transform spawn = controller.LocalPlayerSpawn;
                controller.LocalPlayer.TeleportTo(spawn.position, spawn.rotation);
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
