using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballObj : InteractableObjs
{
    private CannonballStates cannonballStates;
    private Rigidbody rigid;

    public PlayerStates playerState;

    // Initial set up
    private void Awake()
    {
        cannonballStates = GetComponent<CannonballStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public void EnableCannonball(ref PlayerStates playerStates, ref GameObject player)
    {
        // Set up object pooling
        SetPosition(ref player);

        playerStates.playerState = PlayerStates.PlayerState.pCannonball;
        cannonballStates.currentState = CannonballStates.CannonballState.Held;
        playerState = playerStates;

        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
    }

    public override void Interact(GameObject player)
    {
        if(playerState == null) { playerState = player.GetComponent<PlayerStates>(); }

        if (cannonballStates.currentState == CannonballStates.CannonballState.Dropped &&
            playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            cannonballStates.currentState = CannonballStates.CannonballState.Held;
            playerState.playerState = PlayerStates.PlayerState.pCannonball;
            SetPickedUpObjectComponents(ref playerState, ref rigid, gameObject);
        }
    }

    public override void DropItem()
    {
        // Check the cannonball is held
        if (cannonballStates.currentState == CannonballStates.CannonballState.Held)
        {
            this.transform.parent = null;
            cannonballStates.currentState = CannonballStates.CannonballState.Dropped;

            ResetComponents(ref playerState, ref rigid);
        }
    }
}
