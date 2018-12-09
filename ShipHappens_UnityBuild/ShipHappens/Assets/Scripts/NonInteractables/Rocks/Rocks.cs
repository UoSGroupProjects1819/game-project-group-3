using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public enum RockStates { entering, active, exiting };
    public RockStates rockStates;

    public float timer;

    void Update()
    {
        switch (rockStates)
        {
            case RockStates.entering:
                //add count to game manager
                //UI update
                //Play audio
                rockStates = RockStates.active;
                break;
            case RockStates.active:
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    //wait for steering wheel, if not input
                    //damage ship
                    //screenshake
                }
                break;
            case RockStates.exiting:
                //remove from game manager
                break;
        }
    }
}
