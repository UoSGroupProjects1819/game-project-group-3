using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballObj : InteractableObjs
{
    private CannonballStates cannonballStates;
    private Rigidbody rigid;

    public PlayerStates playerStates;
    private PlayerController playerController;

    // Initial set up
    private void Awake()
    {
        cannonballStates = GetComponent<CannonballStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public override void Activate(GameObject otherObject)
    {

    }

    //public void EnableCannonball(ref PlayerStates playerStates, ref GameObject player)
    //{
    //    // Set up object pooling
    //    SetPosition(ref player);

    //    playerStates.playerState = PlayerStates.PlayerState.pCannonball;
    //    cannonballStates.currentState = CannonballStates.CannonballState.Held;
    //    playerState = playerStates;

    //    SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
    //}

    public override void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null)
    {
        SetPosition(ref player);

        playerStates = pStates;

        playerStates.playerState = PlayerStates.PlayerState.pCannonball;
        cannonballStates.currentState = CannonballStates.CannonballState.Held;

        playerController = pController;
        playerController.currentObject = this;

        RotateShoulders(player.transform.GetChild(0).GetChild(0), 90);
        //projector = playerController.transform.GetChild(2).transform.GetChild(1).GetComponent<Projector>();
        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
    }

    public override void DropItem()
    {
        // Check the cannonball is held
        if (cannonballStates.currentState == CannonballStates.CannonballState.Held)
        {
            this.transform.parent = null;
            cannonballStates.currentState = CannonballStates.CannonballState.Dropped;

            RotateShoulders(playerStates.transform.GetChild(0).GetChild(0), -90);

            ResetComponents(ref playerStates, ref rigid);
        }
    }
}
