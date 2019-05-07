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

    public float bailAmount =5 ;

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

    public void IncreaseFloodAmount(int amount)
    {
        currentPosition += new Vector3(currentPosition.x, amount, currentPosition.z);
        currentPosition.x = startPosition.x;
        currentPosition.z = startPosition.z;
    }

    void ClampFloodLevel()
    {
        //clamp min, max y-axis values
        if (currentPosition.y <= startPosition.y - 0.05f)
        {
            currentPosition = startPosition;
        }
        if (currentPosition.y > maxHeight.y - 0.05f)
        {
            currentPosition = maxHeight;
            isGameOver = true;

            if (isTutorial)
                return;
            else if (isTutorial == false)
                distanceTimerBar.isGameOver = true;
        }
    }
}
 