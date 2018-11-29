using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splash : MonoBehaviour
{
    public ParticleSystem PSplayer;
    public ParticleSystem PShat;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            PSplayer.Play();
        }

        if (Input.GetMouseButtonUp(1))
        {
            PShat.Play();
        }
    }
}
