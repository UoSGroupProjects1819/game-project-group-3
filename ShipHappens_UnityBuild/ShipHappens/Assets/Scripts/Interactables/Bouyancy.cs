using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyancy : MonoBehaviour
{
    public Rigidbody rb;

    public float waterLevel;
    public GameObject water;
    public float bouyancyHeightRange;
    public float bounceDamp;
    public Vector3 bouyancyOffset;

    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 uplift;


    private void Start()
    {
        water = GameObject.FindWithTag("FloodWater");
    }

    private void FixedUpdate()
    {
        waterLevel = water.transform.position.y - 0.35f;

        actionPoint = transform.position + transform.TransformDirection(bouyancyOffset);
        forceFactor = 1f - ((actionPoint.y - waterLevel) / bouyancyHeightRange);

        if (forceFactor > 0f)
        {
            uplift = -Physics.gravity * (forceFactor - rb.velocity.y * bounceDamp);
            rb.AddForceAtPosition(uplift, actionPoint);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rb.AddForce(new Vector3(0.3f, 1, 0.5f), ForceMode.Impulse); 
        }


        ///////////// IF FRICTION IS TOO GREAT/LOW IN WATER vs ON DRY DECK
        //if water level is greater than [amount]
            //set rb.drag = [value]


        ///////////// IF FRICTION IS TOO GREAT/LOW IN WATER vs ON DRY DECK
        //if floating too high in water / float rotation weird
            //find height of object on current local:global y axis and set water level minus to 45% of current orientation height
            //could give bouyant objects for states/bools to determine if floating, then lock rotation (prevent mop vertical)
    }

}