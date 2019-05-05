using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodController : MonoBehaviour
{
    public GameObject floodPlane;
    public Vector3 startPosition = new Vector3(-3.863f, 4.0f, 6.85f);
    public Vector3 currentPosition;
    public Vector3 maxHeight = new Vector3(-3.863f, 12.25f, 6.85f);

    public static int numberOfHoles;
    public float floodRate;
    public float floodRateModifier;

    public float bailAmount;

    public bool isGameOver;
    public DistanceTimerBar distanceTimerBar;

    public bool isTutorial;

    private void Start()
    {
        currentPosition = startPosition;
    }

    void Update ()
    {
        floodRate = numberOfHoles * (floodRateModifier * 0.01f * Time.deltaTime);

        floodPlane.transform.position = new Vector3(currentPosition.x, currentPosition.y += floodRate, currentPosition.z);

        ClampFloodLevel();

        Debug.Log("number of holes: " + numberOfHoles);
    }


    public void BailWater()
    {
        currentPosition.y -= bailAmount;
    }

    void ClampFloodLevel()
    {
        //clamp min, max y-axis values
        if (floodPlane.transform.position.y <= startPosition.y - 0.05f)
        {
            floodPlane.transform.position = startPosition;
        }
        if (floodPlane.transform.position.y > maxHeight.y - 0.05f)
        {
            floodPlane.transform.position = maxHeight;
            isGameOver = true;

            if (isTutorial == false)
                distanceTimerBar.isGameOver = true;
        }
    }
}
 