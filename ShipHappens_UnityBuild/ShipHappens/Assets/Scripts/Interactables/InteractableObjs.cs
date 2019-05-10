using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObjs : MonoBehaviour
{ 
    [Header("Pickup Position")]
    public Vector3 PickPosition;
    public Vector3 PickRotation;

    public string interactableTag;

    // Timer variables
    private PlayerStates pState;
    private Rigidbody body;
    protected float taskCountdown;

    // Timer for each class to override, call base.TimerCountdown(taskName, timeToCompleteTask, ref player, ref interactable) before writing new code after
    protected virtual void TimerCountdown(string taskName, float timeToCompleteTask, ref GameObject player, ref GameObject interactable)
    {
        // Performance checks.  Ensure that these are nulled off by children at the end
        if (pState == null) { pState = player.GetComponent<PlayerStates>(); }
        if (body == null) { body = interactable.GetComponent<Rigidbody>(); }

        float taskTime = timeToCompleteTask;
    }

    // Actionable functions
    public virtual void Deactivate() { }
    public abstract void Activate(GameObject otherObject);
    public abstract void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null);
    public abstract void DropItem();
    
    // Set the objects position in the player's hand.  Position is assigned in the inspector of the object.
    public void SetPosition(ref GameObject player)
    {
        transform.parent = player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
    }

    // Set the components correctly when picking up an item
    public void SetPickedUpObjectComponents(ref PlayerStates playerState, ref Rigidbody rb, GameObject itemToHold)
    {
        playerState.itemHeld = itemToHold;

        rb.isKinematic = true;
        rb.detectCollisions = false;
    }

    // When an object is dropped reset the player and objects components
    public void ResetComponents(ref PlayerStates playerState, ref Rigidbody rb, Transform rightArm, PlayerController playerController = null)
    {
        // Player components
        playerState.playerState = PlayerStates.PlayerState.pEmpty;
        playerState.itemHeld = null;
        RotateShoulders(rightArm, playerState);
        playerState = null;

        if (playerController != null)
        {
            Debug.Log("playerController = " + playerController);
            playerController.currentObject = null;
        }

        // Object components
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }

    public void RotateShoulders(Transform rightArm, PlayerStates playerState = null)
    {
        if (playerState.itemHeld == null)
        {            
            rightArm.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (playerState.itemHeld != null)
        {
            rightArm.transform.localEulerAngles = new Vector3(90, 0, 0);
        }
    }
}
