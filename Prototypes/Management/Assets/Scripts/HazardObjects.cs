using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardObjects : MonoBehaviour
{
    public GameObject thisHazard;
    public bool isEnabled;
    public SphereCollider sColl;
    public MeshRenderer mRend;

    public float countdown;
    public float lowRand;
    public float highRand;

    void Start ()
    {
        isEnabled = false;
        countdown = Random.Range(lowRand, highRand);
	}
	

	void Update ()
    {
        countdown -= Time.deltaTime;

        if (countdown < 0)
            isEnabled = true;


        if (!isEnabled)
        {
            sColl.enabled = false;
            mRend.enabled = false; ;
            //thisHazard.SetActive(false);
        }
        if (isEnabled)
        {
            countdown = 150f;
            sColl.enabled = true;
            mRend.enabled = true;
            //thisHazard.SetActive(true);
        }
	}
}
