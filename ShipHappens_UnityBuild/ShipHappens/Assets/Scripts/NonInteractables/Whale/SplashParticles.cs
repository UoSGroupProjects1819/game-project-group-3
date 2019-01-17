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
            Vector3 playerPos = new Vector3(player.transform.position.x, 2.5f, player.transform.position.z);
            playerPS.transform.position = playerPos;

            playerPS.Play();
        }
    }
}
