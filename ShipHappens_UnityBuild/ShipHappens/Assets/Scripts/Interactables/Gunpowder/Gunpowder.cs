using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunpowder : Interactable
{ 
    private GunpowderStates powderStates;
    public PlayerStates playerState;

    // Use this for initialization
    void Start ()
    {
        powderStates = this.GetComponent<GunpowderStates>();
	}

    public override void Action(GameObject player)
    {
        if (powderStates.currentState == GunpowderStates.PowderState.Dropped)
        {
            this.transform.parent = player.transform;
            powderStates.currentState = GunpowderStates.PowderState.Held;
            playerState = this.transform.GetComponentInParent<PlayerStates>();
            playerState.itemHeld = this.gameObject;
            playerState.playerState = PlayerStates.PlayerState.pGunpowder;
        }
    }
            

    public override void DropItem()
    {
        if (powderStates.currentState == GunpowderStates.PowderState.Held)
        {

        }
    }
}
