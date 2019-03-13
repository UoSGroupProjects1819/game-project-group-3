using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenucannons : MonoBehaviour
{
    public ParticleSystem cannonPS;


    public void PlayCannonParticles()
    {
        cannonPS.Play();
    }
}
