using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject floodPlane;
    public Vector3 floodStartPosition = new Vector3(-3.863f, 4.0f, 6.85f);
    public float floodLevel;



    //singleton instance
    public static GameManager Instance { get; private set; }

    //preventing multiple instances
	private void Awake ()
    {
		if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    void Start()
    {
        floodPlane = GameObject.FindGameObjectWithTag("FloodWater");

        floodPlane.transform.position = floodStartPosition;
    }

    void Update ()
    {
        if (floodPlane.transform.position.y < 4)
        {
            floodPlane.transform.position = floodStartPosition;
        }

        floodPlane.transform.position = new Vector3(floodStartPosition.x, floodLevel + 4f, floodStartPosition.z);
	}
}
