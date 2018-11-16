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
        Debug.Log("Got em");
        switch (cannonState)
        {
            case CannonStates.cEmpty:
                break;
            case CannonStates.cCannonBall:
                break;
            case CannonStates.cGunpowder:
                if (currentState == CannonStates.cCannonBall)
                {
                    currentState = CannonStates.cFullyLoaded;
                    break;
                }

                    currentState = CannonStates.cGunpowder;
                

                break;
            case CannonStates.cFullyLoaded:
                Debug.Log("Fully Loaded");
                break;
            default:
                break;
        }
    }
	
}
