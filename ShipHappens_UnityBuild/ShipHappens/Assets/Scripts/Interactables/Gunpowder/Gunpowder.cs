using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunpowder : Interactable
{ 
    private GunpowderStates powderStates;
    public PlayerStates playerState;
    private Rigidbody rb;

    public static GameObject gunPowderPrefab;

    private bool spawning = false;

    public void Spawn(GameObject player)
    {
        playerState = player.GetComponent<PlayerStates>();

        SetPosition(ref player);
        powderStates.currentState = GunpowderStates.PowderState.Held;
        playerState.playerState = PlayerStates.PlayerState.pGunpowder;
        PickedUpComponents(ref playerState, rb, this.gameObject);
    }

    void Awake ()
    {
        powderStates = this.GetComponent<GunpowderStates>();
        rb = this.GetComponent<Rigidbody>();
	}

    public override void Action(GameObject player)
    {
        playerState = player.GetComponent<PlayerStates>();

        if (powderStates.currentState == GunpowderStates.PowderState.Dropped && playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            powderStates.currentState = GunpowderStates.PowderState.Held;         
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
