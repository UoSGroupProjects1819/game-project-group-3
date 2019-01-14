using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Interactable
{

    public CannonState cannonState;
    public PlayerStates playerStates;

    public ParticleSystem cannonFire;

    public cannonUI cannonUI;

    public void Start()
    {
        cannonState = this.GetComponent<CannonState>();
        cannonFire = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    public override void Action(GameObject player)
    {
        playerStates = player.GetComponent<PlayerStates>();

        // Get the interacting players current state
        switch (player.GetComponent<PlayerStates>().playerState)
        {
                // Perform action if the player is holding the cannonball
            case PlayerStates.PlayerState.pCannonball:

                // Check if cannonball is already loaded
                if (cannonState.currentState == CannonState.CannonStates.cCannonBall)
                {
                    Debug.LogWarning("Cannonball already loaded");
                    return;
                }

                OnAction(playerStates);
                cannonState.UpdateState(CannonState.CannonStates.cCannonBall);
                break;
           
                // Perform action if the player is holding the gunpowder
            case PlayerStates.PlayerState.pGunpowder:

                // Check if gunpowder is already loaded
                if (cannonState.currentState == CannonState.CannonStates.cGunpowder)
                {
                    Debug.LogWarning("Gunpowder already loaded");
                    return;
                }

                OnAction(playerStates);
                cannonState.UpdateState(CannonState.CannonStates.cGunpowder);   
                break;

                // Perform action if the player is holding the torch
            case PlayerStates.PlayerState.pTorch:

                if (cannonState.currentState == CannonState.CannonStates.cFullyLoaded)
                {
                    cannonState.currentState = CannonState.CannonStates.cEmpty;
                    cannonFire.Play();               
                }
                else
                {
                    Debug.LogWarning("The cannon is NOT fully loaded");
                }
                break;
                
                // Perfrom actions if the player is empty
            case PlayerStates.PlayerState.pEmpty:

                Debug.LogWarning("Player isn't holding anything!");
                break;
            // If the player is in any other state then break out of the action
            default:

                Debug.LogWarning("That doesn't go in here!");
                break;
        }
    }

    /// <summary>
    /// Reset the player's current state and remove the object that is loaded into the cannon
    /// </summary>
    /// <param name="player"></param>
    private void OnAction(PlayerStates player)
    {
        player.playerState = PlayerStates.PlayerState.pEmpty;
        GameObject interactable = player.GetComponentInChildren<Interactable>().gameObject;
        Destroy(interactable);
    }
}
