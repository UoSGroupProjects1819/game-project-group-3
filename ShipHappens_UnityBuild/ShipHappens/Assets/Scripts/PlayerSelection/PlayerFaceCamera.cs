using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceCamera : MonoBehaviour
{
    public Transform cam;
    public Vector3 offset;

    void LateUpdate()
    {
        transform.LookAt(cam.position + offset);
    }
}
