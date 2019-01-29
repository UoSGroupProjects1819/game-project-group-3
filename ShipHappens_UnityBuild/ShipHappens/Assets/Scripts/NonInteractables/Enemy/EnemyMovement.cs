using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{ 
    public bool isLeftSide;
    public bool isTopSide;

    public float speed;


    private void Awake()
    {

    }

    private void Update()
    {
        if (isLeftSide)
        {
            if (isTopSide)
            {
                transform.Translate(-Vector3.forward * speed *Time.deltaTime);
            }
            else if (!isTopSide)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
        else if (!isLeftSide)
        {
            if (isTopSide)
            {
                transform.Translate(-Vector3.forward * speed * Time.deltaTime);
            }
            else if (!isTopSide)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }

    public void Spawn(bool isLeft, bool isTop)
    {
        isLeftSide = isLeft;
        isTopSide = isTop;
    }
}
