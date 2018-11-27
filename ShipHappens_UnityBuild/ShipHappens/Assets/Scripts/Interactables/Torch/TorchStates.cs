using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchStates : MonoBehaviour {

    public enum TorchState { Dropped, Held }

    public TorchState currentState;

    private void Start()
    {
        currentState = TorchState.Dropped;
    }
}
