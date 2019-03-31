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

    public bool isGameOver;
    public GameObject shipLevel;
    public LevelManager levelManagerScript;

    void Start()
    {
        distanceTimer = maxTimer;
    }

    void Update()
    {
        if (isGameOver)
        {
            //move ship down (sink)
            shipLevel.transform.position += new Vector3(0, -0.2f, 0);
            //when ship has reached y position
            if (shipLevel.transform.position.y < -27)
            {
                levelManagerScript.FadeToLevel(0);
            }
        }

        if (!isGameOver)
        {
            distanceTimer -= Time.deltaTime;

            float percentageFill = Mathf.InverseLerp(0, maxTimer, distanceTimer);
            distanceSlider.value = percentageFill;

            if (distanceTimer <= 0)
            {
                Debug.LogWarning("CONGRATS BOIS, LAND HO! YOU HAVE WON");
                mainCam.transform.position += new Vector3(0, 0, 0.4f);
                
                if (mainCam.transform.position.z > 100)
                {
                    //levelManagerScript.FadeToLevel(REWARD SCENE)
                }
            }
        }
    }
}
