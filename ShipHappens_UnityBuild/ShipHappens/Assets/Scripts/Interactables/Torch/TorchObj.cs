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
        SetPosition(ref player);

        playerStates = pStates;

        playerStates.playerState = PlayerStates.PlayerState.pTorch;
        torchStates.currentState = TorchStates.TorchState.Held;

        playerController = pController;
        playerController.currentObject = this;

        RotateShoulders(player.transform.GetChild(0).GetChild(0), 90);

        //projector = playerController.transform.GetChild(2).transform.GetChild(1).GetComponent<Projector>();
        SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
    }

    public override void DropItem()
    {
        if(torchStates.currentState == TorchStates.TorchState.Held)
        {
            transform.parent = null;
            torchStates.currentState = TorchStates.TorchState.Dropped;
            RotateShoulders(playerStates.transform.GetChild(0).GetChild(0), -90);
            ResetComponents(ref playerStates, ref rigid);
        }
    }
}
