using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    private enum PlayerState { pCannonball, pGunpowder, pMop, pBucket, pWood, pTorch, pEmpty, pHoldingOn };

	// Use this for initialization
	void Start ()
    {
        PlayerState playerState;

        playerState = PlayerState.pEmpty;
	}
}

