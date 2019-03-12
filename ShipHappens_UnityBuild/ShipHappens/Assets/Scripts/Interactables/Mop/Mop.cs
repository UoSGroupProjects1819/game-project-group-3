using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : Interactable
{
    MopStates mopState;
    public Transform holdingPoint;
    public PlayerStates playerState;
    public PlayerController playerController;

    private Rigidbody rb;

    private void Start()
    {
        mopState = this.GetComponent<MopStates>();
        rb = this.GetComponent<Rigidbody>();
    }

    public override void Action(GameObject player)
    {
        playerState = player.GetComponent<PlayerStates>();
        playerController = player.GetComponent<PlayerController>();

        if (mopState.currentState == MopStates.MopState.Dropped && playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {            
            SetPosition(ref player);
            mopState.currentState = MopStates.MopState.Held;          
            playerState.playerState = PlayerStates.PlayerState.pMop;
            //playerController.mop = this;
            PickedUpComponents(ref playerState, rb, this.gameObject);
        }
    }

    public void Cleaning(GameObject hazardToClean)
    {
        // PLAY ANIMATION

        Destroy(hazardToClean);
    }

    public override void DropItem()
    {
        if (mopState.currentState == MopStates.MopState.Held)
        {
            this.transform.parent = null;
            playerController.mop = null;
            mopState.currentState = MopStates.MopState.Dropped;       
            ResetComponents(ref playerState, rb);
        }
    }
}
