using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopObj : InteractableObjs
{
    private MopStates mopStates;
    private PlayerStates playerStates;
    private PlayerController playerController;
    private Rigidbody rigid;

    private void Awake()
    {
        mopStates = GetComponent<MopStates>();
        rigid = GetComponent<Rigidbody>();
    }

    public override void Interact(GameObject player)
    {
        if(playerStates == null) { playerStates = player.GetComponent<PlayerStates>(); }
        if(playerController == null) { playerController = player.GetComponent<PlayerController>(); }

        if (mopStates.currentState == MopStates.MopState.Dropped && playerStates.playerState == PlayerStates.PlayerState.pEmpty)
        {
            SetPosition(ref player);
            mopStates.currentState = MopStates.MopState.Held;
            playerStates.playerState = PlayerStates.PlayerState.pMop;
            playerController.mop = this;
            SetPickedUpObjectComponents(ref playerStates, ref rigid, gameObject);
        }
    }

    public override void DropItem()
    {
        if (mopStates.currentState == MopStates.MopState.Held)
        {
            transform.parent = null;
            playerController.mop = null;
            mopStates.currentState = MopStates.MopState.Dropped;
            ResetComponents(ref playerStates, ref rigid);
        }
    }

    public void CleanPoo()
    { }
}
