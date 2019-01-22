using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelStates : MonoBehaviour
{

    public enum WheelState { Idle, Steering };
    public WheelState wheelStates;

    void Start()
    {
        wheelStates = WheelState.Idle;
    }

    void Update()
    {
        switch (wheelStates)
        {
            case1: 
        }
    }

}
