using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunpowderObj : InteractableObjs
{
    private GunpowderStates gunpowderStates;
    private Rigidbody rigid;

    private PlayerStates playerState;
    private PlayerController playerController;

    private void Awake()
    {
        gunpowderStates = GetComponent<GunpowderStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public override void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null)
    {
        playerState = pStates;

        if (playerState.playerState != PlayerStates.PlayerState.pEmpty)
            return;

        SetPosition(ref player);

        playerState.playerState = PlayerStates.PlayerState.pGunpowder;
        gunpowderStates.currentState = GunpowderStates.PowderState.Held;

        playerController = pController;
        playerController.currentObject = this;

        SetPickedUpObjectComponents(ref playerState, ref rigid, gameObject);
        RotateShoulders(player.transform.GetChild(0).GetChild(0), playerState); 
    }

    public override void DropItem()
    {
        if (gunpowderStates.currentState == GunpowderStates.PowderState.Held)
        {
            transform.parent = null;
            gunpowderStates.currentState = GunpowderStates.PowderState.Dropped;
            
            ResetComponents(ref playerState, ref rigid, playerState.transform.GetChild(0).GetChild(0), playerController);
        }
    }

    public override void Activate(GameObject otherObject) { }
}
