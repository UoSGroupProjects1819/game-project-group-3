using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_timeruitest : MonoBehaviour
{
    public Projector fillProjector;

    public float fullTimer;
    public float timer;

    void Start()
    {
        timer = fullTimer;
    }

    void Update()
    {
        //countdown
        timer = Mathf.Clamp(timer -= Time.deltaTime, 0, fullTimer);

        //find % between full timer and empty timer
        float inverseLerp = Mathf.InverseLerp(fullTimer, 0, timer);
        //this will return decimal between 0-1. shader value is appx 0-2.15 so we multiply return value by 2.15.
        fillProjector.orthographicSize = inverseLerp * 2.15f;
    }
}
