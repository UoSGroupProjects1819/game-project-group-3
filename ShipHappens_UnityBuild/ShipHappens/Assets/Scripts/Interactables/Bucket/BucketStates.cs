using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketStates : MonoBehaviour {

    public enum BucketState { Dropped, Held, Full }

    public BucketState currentState;

    private void Start()
    {
        currentState = BucketState.Dropped;
    }
}
