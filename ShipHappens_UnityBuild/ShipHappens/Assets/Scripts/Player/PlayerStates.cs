using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public enum PlayerState { pCannonball, pGunpowder, pMop, pBucket, pWood, pTorch, pEmpty, pHoldingOn, pWheel, pEdge, pRespawn, pWhaled, pDead};

    public PlayerState playerState;

    public GameObject itemHeld;
    PlayerMovement movement;

    // Events
    public const float maxTimer = 5f;
    public float timer;
    public bool actioning;

    public bool taskComplete = false;

    // Use this for initialization
    void Start ()
    {
        playerState = PlayerState.pEmpty;
        movement = GetComponent<PlayerMovement>();
        timer = maxTimer;
        actioning = false;
	}

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
        {
            actioning = false;
            timer = maxTimer;
        }
    }

    public void Timer()
    {
        if (taskComplete == false && actioning == true)
        {
            movement.canMove = false;
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                taskComplete = true;
                movement.canMove = true;
                actioning = false;
                timer = maxTimer;
            }
            else
            {
                taskComplete = false;
            }
        }
    }

}

