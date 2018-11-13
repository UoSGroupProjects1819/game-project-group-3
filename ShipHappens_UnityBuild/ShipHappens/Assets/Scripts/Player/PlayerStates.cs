using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public enum PlayerState { pCannonball, pGunpowder, pMop, pBucket, pWood, pTorch, pEmpty, pHoldingOn };

    public PlayerState playerState;

    // Use this for initialization
    void Start ()
    {
        playerState = PlayerState.pEmpty;
	}
}

