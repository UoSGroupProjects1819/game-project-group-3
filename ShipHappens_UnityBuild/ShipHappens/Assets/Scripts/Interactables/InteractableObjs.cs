using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjs : MonoBehaviour
{
    // Completion timer
    // Count down timer
    // pick up drop off
    // action
    // set position
    // States 
    // Check usability of timer completed 

    protected float taskCountdown;

    [Header("Pickup Position")]
    public Vector3 PickPosition;
    public Vector3 PickRotation;

    private PlayerStates playerStates;
    private new Rigidbody rigidbody;

    // Timer for each class to override, call base.TimerCountdown(taskName, timeToCompleteTask, ref player, ref interactable) before writing new code after
    protected virtual void TimerCountdown(string taskName, float timeToCompleteTask, ref GameObject player, ref GameObject interactable)
    {
        // Performance checks
        if (playerStates != null) { playerStates = player.GetComponent<PlayerStates>(); }
        if (rigidbody != null) { rigidbody = interactable.GetComponent<Rigidbody>(); }

        float taskTime = timeToCompleteTask;
    }

    public virtual void Interact() { }
    public virtual void Interact(ref GameObject player) { }

    public virtual void DropItem() { }
    public virtual void DropItem(ref GameObject player) { }

    // Set the objects position in the player's hand.  Position is assigned in the inspector of the object.
    public void SetPosition(ref GameObject player)
    {
        transform.parent = player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
    }

    // Set the components correctly when picking up an item
    public void SetPickedUpObjectComponents(ref PlayerStates state, ref Rigidbody rb, GameObject gameObject)
    {
        state.itemHeld = gameObject;

        rb.isKinematic = true;
        rb.detectCollisions = false;
    }

    // When an object is dropped reset the player and objects components
    public void ResetComponents(ref PlayerStates state, ref Rigidbody rb)
    {
        // Player components
        state.playerState = PlayerStates.PlayerState.pEmpty;
        state.itemHeld = null;

        // Object components
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }
}
