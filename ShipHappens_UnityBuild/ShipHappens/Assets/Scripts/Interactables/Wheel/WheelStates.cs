using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelStates : MonoBehaviour
{

    public enum WheelState { active, inactive }

    public WheelState currentState;

    void Start()
    {
        currentState = WheelState.inactive;
    }
}
