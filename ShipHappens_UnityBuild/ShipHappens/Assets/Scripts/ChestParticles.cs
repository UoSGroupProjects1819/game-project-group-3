using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestParticles : MonoBehaviour
{
    public ParticleSystem chestPS;

    public void ChestParticlePlay()
    {
        chestPS.Play();
    }
}
