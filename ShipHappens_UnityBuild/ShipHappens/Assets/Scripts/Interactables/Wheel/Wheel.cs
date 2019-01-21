 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Interactable
{
    public enum WheelStates { Idle, Entering, Active, Exiting };
    public WheelStates wheelStates;

    Rocks rocks;

    public GameObject currPlayer;

    public GameObject shipWheel;
    public float wheelSpeed;

    void Update()
    {
        switch (wheelStates)
        {
            case WheelStates.Idle:
                break;

            case WheelStates.Entering:
                wheelStates = WheelStates.Active;
                break;

            case WheelStates.Active:
                int x = Random.Range(1, 2);
                if (x > 1)
                {
                    shipWheel.transform.Rotate(Vector3.right * wheelSpeed * Time.deltaTime);
                }
                else
                {
                    shipWheel.transform.Rotate(Vector3.right * -wheelSpeed * Time.deltaTime);
                }

                //do time countdown, 
                    //if timer reaches end, stop hazard
                    rocks.rockStates = Rocks.RockStates.exiting;

                if (currPlayer.GetComponent<Rigidbody>().velocity.x > 0 || currPlayer.GetComponent<Rigidbody>().velocity.z > 0)
                {
                    //reset timer

                    ReleaseWheel(currPlayer);
                }

                break;

            case WheelStates.Exiting:
                wheelStates = WheelStates.Idle;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && rocks.rockStates == Rocks.RockStates.active)
        {
            currPlayer = other.gameObject;
        }
    }



    //  NEED TO ONLY  PERFORM ACTION ETC IF ROCK HAZARD IS ACTIVE.



    public override void Action(GameObject player)
    {
        PlayerStates playerState = player.GetComponent<PlayerStates>();

        if (playerState.playerState == PlayerStates.PlayerState.pEmpty)
        {
            playerState.playerState = PlayerStates.PlayerState.pWheel;
            player.transform.LookAt(shipWheel.transform);

            wheelStates = WheelStates.Entering;
        }
    }

    public override void DropItem()
    {
        base.DropItem();
        wheelStates = WheelStates.Exiting;
    }

    public void ReleaseWheel (GameObject player)
    {
        PlayerStates playerState = player.GetComponent<PlayerStates>();

        playerState.playerState = PlayerStates.PlayerState.pEmpty;
        wheelStates = WheelStates.Exiting;
    }
}