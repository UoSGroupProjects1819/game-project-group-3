using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Interactable
{

    public CannonState cannonState;
    public PlayerStates playerStates;

    public void Start()
    {
        cannonState = this.GetComponent<CannonState>();
    }

    public override void Action(GameObject player)
    {
        // Get the interacting players current state
        switch (player.GetComponent<PlayerStates>().playerState)
        {
                // Perform action if the player is holding the cannonball
            case PlayerStates.PlayerState.pCannonball:
                
                break;
           
                // Perform action if the player is holding the gunpowder
            case PlayerStates.PlayerState.pGunpowder:
                playerStates = player.GetComponent<PlayerStates>();
                playerStates.playerState = PlayerStates.PlayerState.pEmpty;
                GameObject gunpowder = player.GetComponentInChildren<Interactable>().gameObject;
                Destroy(gunpowder);
                //cannonState.currentState = CannonState.CannonStates.cGunpowder;
                cannonState.UpdateState(CannonState.CannonStates.cGunpowder);

                
                break;
                // Perform action if the player is holding the torch
            case PlayerStates.PlayerState.pTorch:
                
                break;
                // Perfrom actions if the player is empty
            case PlayerStates.PlayerState.pEmpty:

                break;
            // If the player is in any other state then break out of the action
            default:
                break;
        }
    }
}
