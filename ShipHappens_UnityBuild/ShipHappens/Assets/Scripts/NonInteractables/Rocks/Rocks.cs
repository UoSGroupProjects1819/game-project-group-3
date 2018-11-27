using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public enum RockStates { entering, active, exiting };
    public RockStates rockStates;

    void Start ()
    {
		
	}

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
                //await user steering wheel
                    //if greater than threshold time passes, screenshake, damage ship

                    //if user uses wheel
                break;
            case RockStates.exiting:
                //remove from game manager
                break;
        }
    }
}
