using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public GameObject chest;


	void LateUpdate ()
    {
        transform.LookAt(chest.transform);
	}
}
