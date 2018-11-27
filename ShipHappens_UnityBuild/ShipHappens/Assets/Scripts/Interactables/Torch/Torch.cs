using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Interactable
{

    TorchStates torchState;
    public PlayerStates playerState;
    private Rigidbody rb;

    private void Start()
    {
        torchState = this.GetComponent<TorchStates>();
        rb = this.GetComponent<Rigidbody>();
    }

    public override void Action(GameObject player)
    {
        if (torchState.currentState == TorchStates.TorchState.Dropped)
        {
            this.transform.parent = player.transform.GetChild(1).transform.GetChild(0);
            Debug.Log(this.transform.parent.name);
            //this.transform.SetParent(holdingPoint);
            this.transform.localPosition = (this as Interactable).PickPosition;
            this.transform.localEulerAngles = (this as Interactable).PickRotation;

            torchState.currentState = TorchStates.TorchState.Held;
            playerState = this.transform.GetComponentInParent<PlayerStates>();
            playerState.playerState = PlayerStates.PlayerState.pTorch;

            // Set values for item picked up
            PickedUpComponents(ref playerState, rb, this.gameObject);
        }
    }

    public override void DropItem()
    {
        if (torchState.currentState == TorchStates.TorchState.Held)
        {
            this.transform.parent = null;
            torchState.currentState = TorchStates.TorchState.Dropped;

            ResetComponents(ref playerState, rb);
        }
    }
}
