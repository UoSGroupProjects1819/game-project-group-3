using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject makeMeLookAt;
    public GameObject objToFace;

    void Update()
    {
        transform.LookAt(objToFace.transform);
    }
}
