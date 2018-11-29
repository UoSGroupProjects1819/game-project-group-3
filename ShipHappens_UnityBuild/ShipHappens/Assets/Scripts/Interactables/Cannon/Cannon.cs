using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Interactable
{

    public CannonState cannonState;
    public PlayerStates playerStates;

    public ParticleSystem cannonFire;

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
                Debug.LogWarning("PLAYER HAS LOADED CANNONBALL!");
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
                if (cannonState.currentState == CannonState.CannonStates.cFullyLoaded)
                {
                    cannonState.currentState = CannonState.CannonStates.cEmpty;
                    Instantiate(cannonFire, this.transform.GetChild(0).GetChild(0).position, this.transform.GetChild(0).GetChild(0).rotation, this.transform.GetChild(0).GetChild(0));
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
}
