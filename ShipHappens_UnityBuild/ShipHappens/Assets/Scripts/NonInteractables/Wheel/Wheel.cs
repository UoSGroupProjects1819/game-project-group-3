 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public enum WheelStates { Idle, Steering };
    public WheelStates wheelStates;

    void Update()
    {
        if (player != null)
        {
            //do not reassign player
        }


        switch (wheelStates)
        {
            case WheelStates.Idle:

                break;
            case WheelStates.Steering:

                break;
        }
    }
}
