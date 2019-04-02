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
    public GameObject ship;
    public GameObject floodwater;
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
            ship.transform.position += new Vector3(0, -3, 0) * Time.deltaTime;
            Destroy(floodwater);

            GameObject[] poos;
            poos = GameObject.FindGameObjectsWithTag("poo");
            foreach (GameObject poop in poos)
            {
                poop.gameObject.SetActive(false);
            }

            GameObject[] holes;
            holes = GameObject.FindGameObjectsWithTag("Hole");
            foreach (GameObject hole in holes)
            {
                hole.gameObject.SetActive(false);
            }



            //when ship has reached y position
            if (ship.transform.position.y < -15)
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
                    levelManagerScript.FadeToLevel(5);
                }
            }
        }
    }
}
