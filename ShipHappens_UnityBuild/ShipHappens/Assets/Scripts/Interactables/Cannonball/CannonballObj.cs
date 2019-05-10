using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballObj : InteractableObjs
{
    private CannonballStates cannonballStates;
    private Rigidbody rigid;

    public PlayerStates playerStates;
    private PlayerController playerController;

    private void Awake()
    {
        cannonballStates = GetComponent<CannonballStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public override void Activate(GameObject otherObject)
    {

    }

    public override void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null)
    {
        playerStates = pStates;

        if (playerStates.playerState != PlayerStates.PlayerState.pEmpty)
            return;

        SetPosition(ref player);    

        playerStates.playerState = PlayerStates.PlayerState.pCannonball;
        cannonballStates.currentState = CannonballStates.CannonballState.Held;

        playerController = pController;
        playerController.currentObject = this;

        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
        RotateShoulders(player.transform.GetChild(0).GetChild(0), playerStates);   
    }

    public override void DropItem()
    {
        // Check the cannonball is held
        if (cannonballStates.currentState == CannonballStates.CannonballState.Held)
        {
            this.transform.parent = null;
            cannonballStates.currentState = CannonballStates.CannonballState.Dropped;

            ResetComponents(ref playerStates, ref rigid, playerStates.transform.GetChild(0).GetChild(0), playerController);
        }
    }
}
