using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : Interactable
{
    MopStates mopState;
    public Transform holdingPoint;
    public PlayerStates playerState;

    private Rigidbody rb;

    private void Start()
    {
        mopState = this.GetComponent<MopStates>();
        rb = this.GetComponent<Rigidbody>();
    }

    public override void Action(GameObject player)
    { 
        if (mopState.currentState == MopStates.MopState.Dropped)
        {
            this.transform.parent = player.transform.GetChild(1).transform.GetChild(0);
            Debug.Log(this.transform.parent.name);
            //this.transform.SetParent(holdingPoint);
            this.transform.localPosition = (this as Interactable).PickPosition;
            this.transform.localEulerAngles = (this as Interactable).PickRotation;

            mopState.currentState = MopStates.MopState.Held;
            playerState = this.transform.GetComponentInParent<PlayerStates>();
            playerState.playerState = PlayerStates.PlayerState.pMop;

            // Set values for item picked up
            PickedUpComponents(playerState, rb, this.gameObject);
        }
    }

    public override void DropItem()
    {
        if (mopState.currentState == MopStates.MopState.Held)
        {
            this.transform.parent = null;
            mopState.currentState = MopStates.MopState.Dropped;

            ResetComponents(playerState, rb);
        }
    }
}
