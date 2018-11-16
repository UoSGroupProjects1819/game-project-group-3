using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunpowder : Interactable
{ 
    private GunpowderStates powderStates;
    public PlayerStates playerState;
    private Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        powderStates = this.GetComponent<GunpowderStates>();
        rb = this.GetComponent<Rigidbody>();
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
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }
            

    public override void DropItem()
    {
        if (powderStates.currentState == GunpowderStates.PowderState.Held)
        {
            this.transform.parent = null;
            powderStates.currentState = GunpowderStates.PowderState.Dropped;
            playerState.playerState = PlayerStates.PlayerState.pEmpty;
            playerState.itemHeld = null;
            playerState = null;
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
    }
}
