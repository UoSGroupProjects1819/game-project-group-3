using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSplashNextLevel : MonoBehaviour
{
    public ParticleSystem splash;
    public Animator anim;
    public LevelManager levelManager;

    public void PlayWhaleSplash()
    {
        splash.Play();
    }


    public void LoadTutorialLevel()
    {
        anim.SetBool("PlayWhaleSplash", false);

        //loadgamelevel
        levelManager.FadeToLevel(1);
    }

    public void LoadGameLevel()
    {
        anim.SetBool("PlayWhaleSplash", false);

        //loadgamelevel
        levelManager.FadeToLevel(4);
    }
}
