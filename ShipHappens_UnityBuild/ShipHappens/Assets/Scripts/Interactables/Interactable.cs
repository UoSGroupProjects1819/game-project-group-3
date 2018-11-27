using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Pickup Position")]
    public Vector3 PickPosition;
    public Vector3 PickRotation;

    // Calls the Action function on all interactables
    public virtual void Action(GameObject player) {}

    // Calls the Drop function on all interactables
    public virtual void DropItem() {}

    public void PickedUpComponents(ref PlayerStates playerState, Rigidbody rigidbody, GameObject gameObject)
    {
        // Set player components
        playerState.itemHeld = gameObject;

        // Set Rigidbody components
        rigidbody.isKinematic = true;
        rigidbody.detectCollisions = false;
    }

    // Set items back to original state before being picked up
    public void ResetComponents(ref PlayerStates playerState, Rigidbody rigidbody)
    {
        // Reset playerState components
        playerState.playerState = PlayerStates.PlayerState.pEmpty;
        playerState.itemHeld = null;
        playerState = null;

        // Reset Rigidbody components
        rigidbody.isKinematic = false;
        rigidbody.detectCollisions = true;
    } 
}
