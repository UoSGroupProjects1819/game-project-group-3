using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTimerBar : MonoBehaviour
{
    public float distanceTimer;
    public float maxTimer;

    public Slider distanceSlider;

    void Start()
    {
        distanceTimer = maxTimer;
    }



    void Update()
    {
        distanceTimer -= Time.deltaTime;

        


        float percentageFill = Mathf.InverseLerp(0, maxTimer, distanceTimer);
        distanceSlider.value = percentageFill;
        Debug.Log("percentage fill: " + percentageFill);
    }
}
