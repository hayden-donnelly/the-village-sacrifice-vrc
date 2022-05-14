using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class NoPlayerState : BaseState
{
    // This state does nothing.
    // It is intended for use when there is no local player
    // in order to prevent null reference exceptions.
}
