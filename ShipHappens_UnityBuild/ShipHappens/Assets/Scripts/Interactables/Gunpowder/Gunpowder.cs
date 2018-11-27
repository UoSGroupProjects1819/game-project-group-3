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
            // Set the location to the player's hand on pick up
            this.transform.parent = player.transform.GetChild(1).transform.GetChild(0);
            this.transform.localPosition = (this as Interactable).PickPosition;
            this.transform.localEulerAngles = (this as Interactable).PickRotation;

            powderStates.currentState = GunpowderStates.PowderState.Held;
            playerState = this.transform.GetComponentInParent<PlayerStates>();           
            playerState.playerState = PlayerStates.PlayerState.pGunpowder;

            PickedUpComponents(ref playerState, rb, this.gameObject);
        }
    }
            

    public override void DropItem()
    {
        if (powderStates.currentState == GunpowderStates.PowderState.Held)
        {
            this.transform.parent = null;
            powderStates.currentState = GunpowderStates.PowderState.Dropped;

            ResetComponents(ref playerState, rb);
        }
    }
}
