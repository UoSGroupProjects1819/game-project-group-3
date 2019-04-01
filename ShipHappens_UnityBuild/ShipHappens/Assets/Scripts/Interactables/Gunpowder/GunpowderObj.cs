using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunpowderObj : InteractableObjs
{
    private GunpowderStates gunpowderStates;
    private Rigidbody rigid;

    private PlayerStates playerState;

    private void Awake()
    {
        gunpowderStates = GetComponent<GunpowderStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public void EnableGunpowder(ref PlayerStates playerStates, ref GameObject player)
    {
        SetPosition(ref player);

        playerStates.playerState = PlayerStates.PlayerState.pGunpowder;
        gunpowderStates.currentState = GunpowderStates.PowderState.Held;
        playerState = playerStates;

        SetPickedUpObjectComponents(ref playerState, ref rigid, gameObject);
    }

    public override void Interact(GameObject player)
    {
        if(playerState == null) { playerState = player.GetComponent<PlayerStates>(); }

        if(gunpowderStates.currentState == GunpowderStates.PowderState.Dropped &&
            playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            gunpowderStates.currentState = GunpowderStates.PowderState.Held;
            playerState.playerState = PlayerStates.PlayerState.pGunpowder;
            SetPickedUpObjectComponents(ref playerState, ref rigid, gameObject);
        }
    }

    public override void DropItem()
    {
        if (gunpowderStates.currentState == GunpowderStates.PowderState.Held)
        {
            transform.parent = null;
            gunpowderStates.currentState = GunpowderStates.PowderState.Dropped;

            ResetComponents(ref playerState, ref rigid);
        }
    }


}
