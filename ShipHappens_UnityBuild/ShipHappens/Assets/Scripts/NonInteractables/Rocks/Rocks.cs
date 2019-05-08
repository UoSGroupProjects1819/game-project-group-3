using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocks : Event
{
    public enum RockStates { Idle, Entering, Active, Exiting };
    public RockStates rockStates;

    public float timer = 15;
    public float initialTime = 15;
    public Slider rockSlider;
    public int rockDamage;
    public int tutorialRockDamage;

    public CrowsNestUI CNui;
    public ScreenShake screenShake;
    public Wheel wheel;

    public FloodController floodController;


    public bool isTutorial = false;


    public override void Spawn()
    {
        rockStates = RockStates.Entering;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            Spawn();
        }


        switch (rockStates)
        {
            case RockStates.Idle:
                rockSlider.gameObject.SetActive(false);
                //////////////////////////////////////do nothing until directed by manager
                break;

            case RockStates.Entering:
                /////////////////////////////////////add count to game manager

                if (!isTutorial)
                {
                    //update UI manager
                    CNui.nextAvailableBubbleContents = CNui.ImgRocks;
                    CNui.playNextAvailableBubble = true;
                }

                //update wheel event
                wheel.isInteractable = true;

                timer = initialTime;
                rockStates = RockStates.Active;
                break;

            case RockStates.Active:
                rockSlider.gameObject.SetActive(true);
                timer -= Time.deltaTime;

                float percentageFill = Mathf.InverseLerp(0, initialTime, timer);
                rockSlider.value = percentageFill;

                if (timer <= 0) //wait for steering wheel, if not input
                {
                    screenShake.lightShake = true; //shake screen
                    screenShake.shouldShake = true;

                    if (isTutorial)
                    {
                        floodController.IncreaseFloodAmount(5);                       
                    }
                    else
                    {
                        floodController.IncreaseFloodAmount(3);
                    }

                    rockStates = RockStates.Exiting;
                }
                break;

            case RockStates.Exiting:
                //remove from game manager
                rockSlider.gameObject.SetActive(false);
                rockStates = RockStates.Idle;
                //wheel.wheelStates = Wheel.WheelStates.Exiting;
                wheel.isInteractable = true;
                break;
        }
    }
}
