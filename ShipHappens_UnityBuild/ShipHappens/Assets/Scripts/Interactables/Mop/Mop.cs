using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : Interactable
{
    MopStates mopState;

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
        }
    }

    public override void DropItem()
    {
        if (mopState.currentState == MopStates.MopState.Held)
        {
            this.transform.parent = null;
            mopState.currentState = MopStates.MopState.Dropped;
        }
    }
}
