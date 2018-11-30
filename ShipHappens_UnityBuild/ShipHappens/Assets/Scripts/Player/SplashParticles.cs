using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashParticles : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem playerPS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sea")
        {
            playerPS.transform.position = player.transform.position;
            playerPS.Play();
        }
    }
}
