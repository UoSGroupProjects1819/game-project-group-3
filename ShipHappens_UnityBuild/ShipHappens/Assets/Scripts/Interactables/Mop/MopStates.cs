using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopStates : MonoBehaviour {

    public enum MopState { Dropped, Held }

    public MopState currentState;

    private void Start()
    {
        currentState = MopState.Dropped;
    }
}
