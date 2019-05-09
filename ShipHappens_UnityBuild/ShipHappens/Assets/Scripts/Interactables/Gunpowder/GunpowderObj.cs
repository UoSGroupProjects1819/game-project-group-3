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

    //public void EnableGunpowder(ref PlayerStates playerStates, ref GameObject player)
    //{
    //    SetPosition(ref player);

    //    playerStates.playerState = PlayerStates.PlayerState.pGunpowder;
    //    gunpowderStates.currentState = GunpowderStates.PowderState.Held;
    //    playerState = playerStates;

    //    SetPickedUpObjectComponents(ref playerState, ref rigid, gameObject);
    //}

    public override void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null)
    {
        if (playerState.playerState != PlayerStates.PlayerState.pEmpty)
            return;

        SetPosition(ref player);

        playerState = pStates;

        playerState.playerState = PlayerStates.PlayerState.pGunpowder;
        gunpowderStates.currentState = GunpowderStates.PowderState.Held;

        playerController = pController;
        playerController.currentObject = this;

        RotateShoulders(player.transform.GetChild(0).GetChild(0), 90);
        //projector = playerController.transform.GetChild(2).transform.GetChild(1).GetComponent<Projector>();
        SetPickedUpObjectComponents(ref playerState, ref rigid, gameObject);
    }

    public override void DropItem()
    {
        if (gunpowderStates.currentState == GunpowderStates.PowderState.Held)
        {
            transform.parent = null;
            gunpowderStates.currentState = GunpowderStates.PowderState.Dropped;

            RotateShoulders(playerState.transform.GetChild(0).GetChild(0), -90);

            ResetComponents(ref playerState, ref rigid);
        }
    }

    public override void Activate(GameObject otherObject) { }
}
