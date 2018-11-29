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
        playerStates = player.GetComponent<PlayerStates>();

        // Get the interacting players current state
        switch (player.GetComponent<PlayerStates>().playerState)
        {
                // Perform action if the player is holding the cannonball
            case PlayerStates.PlayerState.pCannonball:
                playerStates.playerState = PlayerStates.PlayerState.pEmpty;
                GameObject cannonball = player.GetComponentInChildren<Interactable>().gameObject;
                Destroy(cannonball);
                cannonState.UpdateState(CannonState.CannonStates.cCannonBall);
                break;
           
                // Perform action if the player is holding the gunpowder
            case PlayerStates.PlayerState.pGunpowder:
                playerStates.playerState = PlayerStates.PlayerState.pEmpty;
                GameObject gunpowder = player.GetComponentInChildren<Interactable>().gameObject;
                Destroy(gunpowder);
                cannonState.UpdateState(CannonState.CannonStates.cGunpowder);   
                break;

                // Perform action if the player is holding the torch
            case PlayerStates.PlayerState.pTorch:
                playerStates.playerState = PlayerStates.PlayerState.pEmpty;
                GameObject torch = player.GetComponentInChildren<Interactable>().gameObject;
                Destroy(torch);
                // TODO: FIRE CANNON
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
