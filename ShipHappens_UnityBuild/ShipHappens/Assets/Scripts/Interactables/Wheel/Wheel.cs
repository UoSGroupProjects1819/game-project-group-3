 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Interactable
{
    public enum WheelStates { Idle, Active, Exiting };
    public WheelStates wheelStates;

    public bool isInteractable = false;

    public Rocks rocks;
    public GameObject shipWheel;
    //public float wheelSpeed = 5;

    public GameObject currPlayer = null;

    public float timer = 4;
    public float initialTime = 4;

      

    void Update()
    {
        switch (wheelStates)
        {
            case WheelStates.Idle:
                break;

            case WheelStates.Active:
                timer -= Time.deltaTime; //if player completes timer, rock state exiting, wheel state exiting
                if (timer <= 0)
                {
                    rocks.rockStates = Rocks.RockStates.Exiting;
                    wheelStates = WheelStates.Exiting;
                }

                //int x = Random.Range(1, 2); //spins wheel in random dir
                //if (x > 1)
                //{
                //    shipWheel.transform.Rotate(Vector3.up * wheelSpeed * Time.deltaTime);
                //}
                //else
                //{
                //    shipWheel.transform.Rotate(Vector3.up * -wheelSpeed * Time.deltaTime);
                //}

                Debug.Log("player x vel: " + currPlayer.GetComponent<Rigidbody>().velocity.x);
                if (currPlayer.GetComponent<Rigidbody>().velocity.x > 0.0000015f || currPlayer.GetComponent<Rigidbody>().velocity.z > 0.0000015f) //if player moves during timer, timer resets + action is cancelled + wheel states exiting
                {
                    wheelStates = WheelStates.Exiting;
                }
                break;

            case WheelStates.Exiting:
                timer = initialTime;
                ReleaseWheel(currPlayer);
                wheelStates = WheelStates.Idle;
                break;
        }
    }

    public override void Action(GameObject player)
    {
        if (isInteractable == false)
        {
            return;
        }
        else
        {
            currPlayer = player;

            PlayerStates playerState = player.GetComponent<PlayerStates>();

            if (playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                playerState.playerState = PlayerStates.PlayerState.pWheel;

                wheelStates = WheelStates.Active;
            }
        }
    }

    public override void DropItem()
    {
        base.DropItem();
        wheelStates = WheelStates.Exiting;
    }

    public void ReleaseWheel (GameObject player)
    {
        if (isInteractable == false)
        {
            return;
        }
        else
        {
          PlayerStates playerState = player.GetComponent<PlayerStates>();
          playerState.playerState = PlayerStates.PlayerState.pEmpty;

          currPlayer = null;            
        }
    }
}