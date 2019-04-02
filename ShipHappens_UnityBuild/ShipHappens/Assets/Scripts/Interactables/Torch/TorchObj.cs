using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchObj : InteractableObjs
{
    private TorchStates torchStates;
    private PlayerStates playerStates;
    private Rigidbody rigid;

    private void Start()
    {
        torchStates = GetComponent<TorchStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public override void Interact(GameObject player)
    {
        if(playerStates == null) { playerStates = player.GetComponent<PlayerStates>(); }

        if(torchStates.currentState == TorchStates.TorchState.Dropped && playerStates.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            torchStates.currentState = TorchStates.TorchState.Held;
            playerStates.playerState = PlayerStates.PlayerState.pTorch;
            SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
        }
    }

    public override void DropItem()
    {
        if(torchStates.currentState == TorchStates.TorchState.Held)
        {
            transform.parent = null;
            torchStates.currentState = TorchStates.TorchState.Dropped;
            ResetComponents(ref playerStates, ref rigid);
        }
    }
}
