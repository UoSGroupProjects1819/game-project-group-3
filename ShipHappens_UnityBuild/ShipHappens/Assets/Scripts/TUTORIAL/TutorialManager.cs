using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int stage = 0;

    //PLAYERS
    public GameObject[] players;

    //CROWS NEST
    public CrowsNestUI crowsNest;
    public Animator CNanim;
    public Sprite crowsNestImg;

    //DPAD MENU
    public CanvasGroup dpadMenu;

    //PULSE PARTICLE SYSTEM
    public ParticleSystem pulseCircle;

    //TOP LEFT CANNON
    public GameObject cannonTopLeft;
    private Animator cannonTopLeftAnim;
    public Sprite cannonImg;

    //SHIP HOLD
    public GameObject shipHold;
    public Animator shipHoldAnim;
    public Sprite shipHoldImg;

    //GUNPOWDER
    public GameObject gunpowder;
    public Sprite gunpowderImg;
    public ParticleSystem gunpowderCircle;

    //CANNONBALL
    public GameObject cannonball;
    public Sprite cannonballImg;
    public ParticleSystem cannonballCircle;

    //WOOD
    public GameObject wood;
    public Sprite woodImg;
    public ParticleSystem woodCircle;

    //ENEMY
    public GameObject enemyShip;
    public Sprite enemyImg;


    void Start()
    {


    }


    void Update()
    {
        TutorialSteps();
    }

    void TutorialSteps()
    {
        switch (stage)
        {
            case 0:
                crowsNestImg = enemyImg;
                CNanim.SetBool("PlayTutorialBubble", true);
                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    stage = 1;
                }
                break;

            case 1:
                crowsNestImg = shipHoldImg;
                shipHoldAnim.SetBool("PlayTutorialHold", true);
                CNanim.SetBool("PlayTutorialBubble", true);
                stage = 2;
                break;

            case 2:
                if (dpadMenu.alpha > 0.33f)
                {
                    shipHoldAnim.SetBool("PlayTutorialHold", false);
                    crowsNestImg = cannonballImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    cannonballCircle.gameObject.SetActive(true);
                    cannonballCircle.Play();

                    foreach (GameObject player in players)
                    {
                        if (player.GetComponent<PlayerStates>().playerState == PlayerStates.PlayerState.pCannonball)
                        {
                            stage = 3;
                        }
                    }
                }
                else
                {
                    cannonballCircle.gameObject.SetActive(false);
                }
                break;

            case 3:

                break;
        }
    }

    public void StopTutorialBubble()
    {
        CNanim.SetBool("PlayTutorialBubble", false);
    }
}
