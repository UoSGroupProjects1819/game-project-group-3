using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public enum RockStates { Idle, Entering, Active, Exiting };
    public RockStates rockStates;

    public float timer = 15;
    public float initialTime = 15;

    public CrowsNestUI CNui;
    public ScreenShake screenShake;
    public Wheel wheel;


    void Update()
    {
        switch (rockStates)
        {
            case RockStates.Idle:
                //////////////////////////////////////do nothing until directed by manager
                break;

            case RockStates.Entering:
                /////////////////////////////////////add count to game manager

                //update UI manager
                CNui.nextAvailableBubbleContents = CNui.ImgRocks;
                CNui.playNextAvailableBubble = true;

                //update wheel event
                wheel.isInteractable = true;

                timer = initialTime;
                rockStates = RockStates.Active;
                break;

            case RockStates.Active:
                timer -= Time.deltaTime;
                if (timer <= 0) //wait for steering wheel, if not input
                {
                    screenShake.lightShake = true; //shake screen
                    screenShake.shouldShake = true;
  
                    //damage ship
                        //water level manager ++

                    rockStates = RockStates.Exiting;
                }
                break;

            case RockStates.Exiting:
                //remove from game manager

                wheel.isInteractable = false; //disable player-wheel interaction
                rockStates = RockStates.Idle;
                wheel.wheelStates = Wheel.WheelStates.Idle;
                break;
        }
    }
}
