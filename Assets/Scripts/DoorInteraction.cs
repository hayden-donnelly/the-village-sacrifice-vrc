using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DoorInteraction : UdonSharpBehaviour
{
    [SerializeField] private Transform target;

    public override void Interact()
    {
        Networking.LocalPlayer.TeleportTo(target.position, target.rotation);
    }

}
