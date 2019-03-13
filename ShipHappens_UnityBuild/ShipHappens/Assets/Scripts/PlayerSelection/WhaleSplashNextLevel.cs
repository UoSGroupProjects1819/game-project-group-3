using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSplashNextLevel : MonoBehaviour
{
    public ParticleSystem splash;
    public Animator anim;

    public void PlayWhaleSplash()
    {
        splash.Play();
    }

    public void LoadGameLevel()
    {
        anim.SetBool("PlayWhaleSplash", false);
        //loadgamelevel
        Debug.Log("load the game");
    }
}
