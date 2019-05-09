using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunkerDown : InteractableObjs
{

    GameObject playerObj; 

    public override void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null)
    {
        PlayerStates playerState = pStates;

        if (playerState.playerState != PlayerStates.PlayerState.pEmpty)
            return;

        if (playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            player.transform.parent = this.transform;
            playerState.playerState = PlayerStates.PlayerState.pHoldingOn;
            playerState.itemHeld = this.gameObject;
            player.GetComponent<PlayerMovement>().canMove = false;
            playerObj = player;
        }
    }

    public override void Activate(GameObject otherObject) { }

    public override void DropItem()
    {
        
    }

    public void ReleaseMast(GameObject player)
    {
        PlayerStates playerState = player.GetComponent<PlayerStates>();

        player.transform.parent = null;
        playerState.playerState = PlayerStates.PlayerState.pEmpty;
        playerState.itemHeld = null;
        player.GetComponent<PlayerMovement>().canMove = true;
    }
}


// Need to handle all players
// Array
// Handle which players drop off