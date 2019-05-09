 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : InteractableObjs
{
    public enum WheelStates { Idle, Active, Exiting };
    public WheelStates wheelStates;

    public bool isInteractable = false;

    public Rocks rocks;
    public GameObject shipWheel;
    //public float wheelSpeed = 5;

    public GameObject currPlayer = null;

    Projector projector;

    PlayerStates playerState;
    PlayerController playerController;

    public float timer = 4;
    public float initialTime = 4;

    public override void Activate(GameObject otherObject) {}
    public override void Deactivate() { wheelStates = WheelStates.Exiting; playerController.currentObject = null; }

    void Update()
    {
        switch (wheelStates)
        {
            case WheelStates.Idle:
                break;

            case WheelStates.Active:
                timer -= Time.deltaTime; //if player completes timer, rock state exiting, wheel state exiting

                float inverseLerp = Mathf.InverseLerp(initialTime, 0, timer);

                projector.orthographicSize = inverseLerp * 2.15f;

                if (timer <= 0)
                {
                    rocks.rockStates = Rocks.RockStates.Exiting;
                    Deactivate();
                }

                //if (currPlayer.GetComponent<Rigidbody>().velocity.x > 0.0000015f || currPlayer.GetComponent<Rigidbody>().velocity.z > 0.0000015f) //if player moves during timer, timer resets + action is cancelled + wheel states exiting
                //{
                //    wheelStates = WheelStates.Exiting;
                //}
                break;

            case WheelStates.Exiting:
                timer = initialTime;
                projector.orthographicSize = 2.1f;
                ReleaseWheel(currPlayer);
                wheelStates = WheelStates.Idle;
                break;
        }
    }

    public override void Pickup(GameObject player, PlayerController pController = null, PlayerStates pStates = null)
    {
        if (playerStates.playerState != PlayerStates.PlayerState.pEmpty)
            return;

        if (isInteractable == false)
        {
            return;
        }
        else
        {
            currPlayer = player;

            playerState = pStates;
            playerController = pController;
            playerController.currentObject = this;

            if (playerState.playerState == PlayerStates.PlayerState.pEmpty)
            {
                playerState.playerState = PlayerStates.PlayerState.pWheel;

                projector = playerController.transform.GetChild(2).transform.GetChild(1).GetComponent<Projector>();

                wheelStates = WheelStates.Active;
            }
        }
    }

    public override void DropItem()
    {
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
          playerState.playerState = PlayerStates.PlayerState.pEmpty;

          currPlayer = null;            
        }
    }
}