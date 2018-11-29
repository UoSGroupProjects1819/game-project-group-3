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
        playerState = player.GetComponent<PlayerStates>();

        if (torchState.currentState == TorchStates.TorchState.Dropped && playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            torchState.currentState = TorchStates.TorchState.Held;
            playerState.playerState = PlayerStates.PlayerState.pTorch;
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
