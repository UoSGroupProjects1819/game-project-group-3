using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunkerDown : Interactable
{
    public override void Action(GameObject player)
    {
        PlayerStates playerState = player.GetComponent<PlayerStates>();

        if (playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            player.transform.parent = this.transform;
            playerState.playerState = PlayerStates.PlayerState.pHoldingOn;
            playerState.itemHeld = this.gameObject;
            player.GetComponent<PlayerMovement>().canMove = false;
        }
    }

    public override void DropItem()
    {
        base.DropItem();
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