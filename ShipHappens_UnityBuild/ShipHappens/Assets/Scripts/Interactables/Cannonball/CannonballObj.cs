using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballObj : InteractableObjs
{
    private CannonballStates states;
    private Rigidbody rigid;

    // Initial set up
    private void Awake()
    {
        states = GetComponent<CannonballStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public void EnableCannonball(ref PlayerStates playerStates, ref GameObject player)
    {
        // Set up object pooling
        SetPosition(ref player);

        playerStates.playerState = PlayerStates.PlayerState.pCannonball;
        states.currentState = CannonballStates.CannonballState.Held;

        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
    }

    public override void Interact(ref GameObject player, ref PlayerStates playerState)
    {
        if (states.currentState == CannonballStates.CannonballState.Dropped &&
            playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            states.currentState = CannonballStates.CannonballState.Held;
            playerState.playerState = PlayerStates.PlayerState.pCannonball;
            SetPickedUpObjectComponents(ref playerState, ref rigid, gameObject);
        }
    }

    public override void DropItem(ref PlayerStates playerStates)
    {
        // Check the cannonball is held
        if (states.currentState == CannonballStates.CannonballState.Held)
        {
            this.transform.parent = null;
            states.currentState = CannonballStates.CannonballState.Dropped;

            ResetComponents(ref playerStates, ref rigid);
        }
    }

}
