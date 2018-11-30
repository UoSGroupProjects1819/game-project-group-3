using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestParticles : MonoBehaviour
{
    public ParticleSystem chestPS;
    public ParticleSystem rewardPS;
    public SpriteRenderer rewardSprite;

    private void Start()
    {
        rewardSprite.enabled = false;
    }

    public void ChestParticlePlay()
    {
        chestPS.Play();
    }

    public void RewardParticlePlay()
    {
        rewardSprite.enabled = true;
        rewardPS.Play();
    }
}
