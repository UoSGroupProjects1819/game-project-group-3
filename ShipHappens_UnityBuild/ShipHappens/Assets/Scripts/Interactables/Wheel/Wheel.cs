//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Wheel : Interactable
//{
//    WheelStates wheelState;
//    public PlayerStates playerState;

//    public GameObject wheel;

//    public override void Action(GameObject player)
//    {
//        if (wheelState.currentState == WheelStates.WheelState.Unattended)
//        {
//            player.transform.parent = this.transform;
//            wheelState.currentState = WheelStates.WheelState.InUse;
//            playerState = this.transform.GetComponentInParent<PlayerStates>();
//            playerState.playerState = PlayerStates.PlayerState.pMop;

//            // Set values for item picked up
//            PickedUpComponents(playerState, rb, this.gameObject);
//        }
//    }

//    public override void DropItem()
//    {
//        if (wheelState.currentState == WheelStates.WheelState.InUse)
//        {
//            this.transform.parent = null;
//            mopState.currentState = MopStates.MopState.Dropped;

//            ResetComponents(playerState, rb);
//        }
//    }
//}
