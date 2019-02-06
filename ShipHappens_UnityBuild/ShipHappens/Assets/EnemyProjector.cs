using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjector : MonoBehaviour
{
    public Projector shrinkingProjector;
    public GameObject cannonBall;

    private void Start()
    {
        shrinkingProjector = transform.GetChild(0).GetComponent<Projector>();
        cannonBall = transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        //shrink projector as cannonball y-axis drops
        shrinkingProjector.orthographicSize = (cannonBall.transform.position.y + 8) / 8;

        //Mathf.Clamp(shrinkingProjector.orthographicSize, 0, 9);
        if (shrinkingProjector.orthographicSize > 8.33f)
        {
            shrinkingProjector.orthographicSize = 8.33f;
        }

        if (shrinkingProjector.orthographicSize < 3.31f)
        {
            shrinkingProjector.orthographicSize = 3.31f;
        }

        

    }





}
