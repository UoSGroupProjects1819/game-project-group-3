using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonState : MonoBehaviour {

    public enum CannonStates { cEmpty, cGunpowder, cCannonBall, cFullyLoaded };
    public CannonStates currentState;

    // Use this for initialization
    void Start () {
        currentState = CannonStates.cEmpty;
	}

    // Check what state the cannon is in to perform certain actions
    public void UpdateState(CannonStates cannonState)
    {
        switch (cannonState)
        {
            // If the player is holding nothing and the cannon is holding nothing
            case CannonStates.cEmpty:
                break;
            // If the player is loading in the cannonball
            case CannonStates.cCannonBall:
                if (currentState == CannonStates.cGunpowder)
                {
                    currentState = CannonStates.cFullyLoaded;
                    break;
                }

                currentState = CannonStates.cCannonBall;
                break;
            // If the player is loading in the gunpowder
            case CannonStates.cGunpowder:
                if (currentState == CannonStates.cCannonBall)
                {
                    currentState = CannonStates.cFullyLoaded;
                    break;
                }

                currentState = CannonStates.cGunpowder;            
                break;
            // If the cannon is ready to be fired, run code when player has the torch
            case CannonStates.cFullyLoaded:
                currentState = CannonStates.cFullyLoaded;
                break;
            // Fallback case incase of an error
            default:
                break;
        }
    }
	
}
