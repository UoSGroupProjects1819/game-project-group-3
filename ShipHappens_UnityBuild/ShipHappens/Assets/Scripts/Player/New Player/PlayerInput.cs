using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;

    private string horizontalAxis, verticalAxis;
    private string aButton, bButton;
    private string horizontalDPad, verticalDPad;
    public int playerNumber;

    static int numOfPlayers = 0;

    public float horizontalMovement { get; set; }
    public float verticalMovement { get; set; }

    public enum Button { A, B }

    private void Start()
    {
        numOfPlayers++;
        playerNumber = numOfPlayers;

        SetControllerNumber(playerNumber);
        Debug.Log(horizontalAxis);
    }

    // Check if either button has been pressed
    internal bool ButtonIsDown(Button button)
    {
        switch(button)
        {
            case Button.A:
                return Input.GetButtonDown(aButton);
            case Button.B:
                return Input.GetButtonDown(bButton);
        }

        return false;
    }

    internal void SetControllerNumber(int number)
    {
        horizontalAxis = "Horizontal_P" + playerNumber;
        verticalAxis = "Vertical_P" + playerNumber;
        aButton = "A_P" + playerNumber;
        bButton = "B_P" + playerNumber;
        horizontalDPad = "DPadHorizontal_P" + playerNumber;
        verticalDPad = "DPadVertical_P" + playerNumber;
    }

    private void FixedUpdate()
    {

        //if (Input.GetButtonDown(aButton))
        //{
        //    Debug.Log("A pressed in input");
        //}

        if (playerNumber > 0)
        {
            horizontalMovement = Input.GetAxis(horizontalAxis);
            verticalMovement = Input.GetAxis(verticalAxis);
        }
    }
}
