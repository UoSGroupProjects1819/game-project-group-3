using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public enum PlayerState { pCannonball, pGunpowder, pMop, pBucket, pWood, pTorch, pEmpty, pHoldingOn, pWheel };

    public PlayerState playerState;

    public GameObject itemHeld;

    // Use this for initialization
    void Start ()
    {
        playerState = PlayerState.pEmpty;
	}

}

