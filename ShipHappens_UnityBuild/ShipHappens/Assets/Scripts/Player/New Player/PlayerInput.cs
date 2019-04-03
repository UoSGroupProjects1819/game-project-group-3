using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int playerNumber;
    static int numOfPlayers = 0;

    private string horizontalAxis, verticalAxis;
    public float HorizontalMovement { get; set; }
    public float VerticalMovement { get; set; }

    private string aButton, bButton;
    public enum Button { A, B }

    private string horizontalDPad, verticalDPad;
    public string GetHorizontalDPad() { return horizontalDPad; }
    public string GetVerticalDPad() { return verticalDPad; }

    private void Start()
    {
        // Static variable that will count the amount of players in the game, allows an easy way to add more players to the game
        numOfPlayers++;
        playerNumber = numOfPlayers;

        // Assign the players input settings with their player number
        SetControllerNumber(playerNumber);
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
        if (playerNumber > 0)
        {
            HorizontalMovement = Input.GetAxis(horizontalAxis);
            if (Mathf.Abs(HorizontalMovement) < 0.1) { HorizontalMovement = 0; }
            VerticalMovement = Input.GetAxis(verticalAxis);
            if (Mathf.Abs(VerticalMovement) < 0.1) { VerticalMovement = 0; }
        }
    }
}
