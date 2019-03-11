using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjs : MonoBehaviour
{
    // States 

    protected float taskCountdown;

    [Header("Pickup Position")]
    public Vector3 PickPosition;
    public Vector3 PickRotation;

    // Timer variables
    private PlayerStates playerStates;
    private Rigidbody body;

    // Timer for each class to override, call base.TimerCountdown(taskName, timeToCompleteTask, ref player, ref interactable) before writing new code after
    protected virtual void TimerCountdown(string taskName, float timeToCompleteTask, ref GameObject player, ref GameObject interactable)
    {
        // Performance checks
        if (playerStates == null) { playerStates = player.GetComponent<PlayerStates>(); }
        if (body == null) { body = interactable.GetComponent<Rigidbody>(); }

        float taskTime = timeToCompleteTask;
    }

    public virtual void Interact() { }
    public virtual void Interact(ref GameObject player) { }
    public virtual void Interact(ref PlayerStates playerStates) { }
    public virtual void Interact(ref GameObject player, ref PlayerStates playerStates) { }

    public virtual void DropItem() { }
    public virtual void DropItem(ref GameObject player) { }
    public virtual void DropItem(ref PlayerStates playerStates) { }
    public virtual void DropItem(ref GameObject player, ref PlayerStates playerStates) { }
    
    // Set the objects position in the player's hand.  Position is assigned in the inspector of the object.
    public void SetPosition(ref GameObject player)
    {
        transform.parent = player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
    }

    // Set the components correctly when picking up an item
    public void SetPickedUpObjectComponents(ref PlayerStates state, ref Rigidbody rb, GameObject itemToHold)
    {
        state.itemHeld = itemToHold;

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
