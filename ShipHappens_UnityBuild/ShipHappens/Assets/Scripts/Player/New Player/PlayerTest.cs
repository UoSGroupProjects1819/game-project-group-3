using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.ButtonIsDown(PlayerInput.Button.A))
        {
            Debug.Log(playerInput.playerNumber + " A is pressed");
        }

        if(playerInput.ButtonIsDown(PlayerInput.Button.B))
        {
            Debug.Log(playerInput.playerNumber + " B is pressed");
        }
    }
}
