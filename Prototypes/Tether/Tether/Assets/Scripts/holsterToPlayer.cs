using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holsterToPlayer : MonoBehaviour
{
    public GameObject child;
    public GameObject parent;

	void Update ()
    {
        child.transform.position = parent.transform.position;
	}
}
