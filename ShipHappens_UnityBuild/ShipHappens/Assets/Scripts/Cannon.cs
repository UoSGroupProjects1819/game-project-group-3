using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Interactable
{
    public override void Action(GameObject player)
    {
        // Get the interacting players current state
        switch (player.GetComponent<PlayerStates>().playerState)
        {
            // Perform action if the player is holding the cannonball
            case PlayerStates.PlayerState.pCannonball:
                Debug.Log("Player has placed Cannonball");
                break;
            case PlayerStates.PlayerState.pEmpty:
                Debug.Log("Player is empty at cannon");
                break;
                // Perform action if the player is holding the gunpowder
            case PlayerStates.PlayerState.pGunpowder:
                Debug.Log("Player has placed Gunpowder");
                break;
                // Perform action if the player is holding the torch
            case PlayerStates.PlayerState.pTorch:
                Debug.Log("Player has used the torch.  FIRE");
                break;
                // If the player is in any other state then break out of the action
            default:
                break;
        }
    }
}
