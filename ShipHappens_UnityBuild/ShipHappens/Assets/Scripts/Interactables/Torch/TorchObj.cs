using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchObj : InteractableObjs
{
    private TorchStates torchStates;
    private PlayerStates playerStates;
    private PlayerController playerController;
    private Rigidbody rigid;

    public override void Activate(GameObject otherObject) { }
    public override void Deactivate() { }

    private void Start()
    {
        torchStates = GetComponent<TorchStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public override void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null)
    {
        playerStates = pStates;

        if (playerStates.playerState != PlayerStates.PlayerState.pEmpty)
            return;

        SetPosition(ref player);

        playerStates.playerState = PlayerStates.PlayerState.pTorch;
        torchStates.currentState = TorchStates.TorchState.Held;

        playerController = pController;
        playerController.currentObject = this;

        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
        RotateShoulders(player.transform.GetChild(0).GetChild(0), playerStates);        
    }
    public override void DropItem()
    {
        if(torchStates.currentState == TorchStates.TorchState.Held)
        {
            transform.parent = null;
            torchStates.currentState = TorchStates.TorchState.Dropped;
           
            ResetComponents(ref playerStates, ref rigid, playerStates.transform.GetChild(0).GetChild(0), playerController);
        }
    }
}
