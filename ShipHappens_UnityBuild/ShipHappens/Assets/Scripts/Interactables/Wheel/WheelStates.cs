using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelStates : MonoBehaviour {

    public enum WheelState { InUse, Unattended }

    public WheelState currentState;

    void Start()
    {
        currentState = WheelState.Unattended;
    }
}
