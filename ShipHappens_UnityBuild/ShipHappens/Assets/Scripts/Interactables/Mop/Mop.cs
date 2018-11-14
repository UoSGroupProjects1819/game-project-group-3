using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : Interactable
{
    MopStates mopState;
    public PlayerStates playerState;

    private void Start()
    {
        mopState = this.GetComponent<MopStates>();
    }

    public override void Action(GameObject player)
    { 
        if (mopState.currentState == MopStates.MopState.Dropped)
        {
            this.transform.parent = player.transform;
            mopState.currentState = MopStates.MopState.Held;
            playerState = this.transform.GetComponentInParent<PlayerStates>();
            playerState.itemHeld = this.gameObject;
            playerState.playerState = PlayerStates.PlayerState.pMop;
        }
    }

    public override void DropItem()
    {
        if (mopState.currentState == MopStates.MopState.Held)
        {
            this.transform.parent = null;
            mopState.currentState = MopStates.MopState.Dropped;
            playerState.playerState = PlayerStates.PlayerState.pEmpty;
            playerState.itemHeld = null;
            playerState = null;
        }
    }
}
