using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject objToFace;

    void Update()
    {
        transform.LookAt(objToFace.transform, new Vector3(0f,1f,0f));
    }
}
