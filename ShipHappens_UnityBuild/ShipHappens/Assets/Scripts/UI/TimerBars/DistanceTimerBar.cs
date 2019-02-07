using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTimerBar : MonoBehaviour
{
    public float distanceTimer;
    public float maxTimer;
    public Slider distanceSlider;

    public Camera mainCam;
    public ParticleSystem endPS;


    void Start()
    {
        distanceTimer = maxTimer;
    }

    void Update()
    {
        distanceTimer -= Time.deltaTime;

        float percentageFill = Mathf.InverseLerp(0, maxTimer, distanceTimer);
        distanceSlider.value = percentageFill;

        if (distanceTimer <= 0)
        {
            Debug.LogWarning("CONGRATS BOIS, LAND HO! YOU HAVE WON");
            mainCam.transform.position += new Vector3(0, 0, 0.4f);
            //endPS.Play();
            //end the scene
            //load reward scene
        }
    }
}
