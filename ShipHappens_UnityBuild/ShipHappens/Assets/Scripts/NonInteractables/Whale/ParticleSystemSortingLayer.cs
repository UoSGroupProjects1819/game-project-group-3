using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSortingLayer : MonoBehaviour
{
    public ParticleSystemRenderer particleSystemRend;

	void Start ()
    {
        particleSystemRend.sortingOrder = 0;
    }
	
}
