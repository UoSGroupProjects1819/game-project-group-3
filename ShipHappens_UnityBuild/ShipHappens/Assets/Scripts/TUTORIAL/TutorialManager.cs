using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public int stage = 0;

    private float timer;

    //PLAYERS
    public GameObject[] players;

    //CROWS NEST
    public bool playBubble;
    public bool bubbleHasPlayed = false;
    public CrowsNestUI crowsNest;
    public Animator CNanim;
    public Image TutorialBubble;
    public Image tutorialBubbleInterior;

    //DPAD MENU
    public CanvasGroup dpadMenu;

    //TOP LEFT CANNON
    public GameObject cannonTopLeft;
    public Animator cannonTopLeftAnim;
    public Sprite cannonImg;

    //SHIP HOLD
    //public GameObject shipHold;
    public Animator shipHoldAnim;
    public Sprite shipHoldImg;

    //GUNPOWDER
    //public GameObject gunpowder;
    public Sprite gunpowderImg;
    public ParticleSystem gunpowderCircle;

    //CANNONBALL
    //public GameObject cannonball;
    public Sprite cannonballImg;
    public ParticleSystem cannonballCircle;

    //WOOD
    //public GameObject wood;
    public Sprite woodImg;
    public ParticleSystem woodCircle;

    //TORCH
    public GameObject torch;
    public Animator torchAnim;
    public Sprite torchImg;

    //ENEMY
    public GameObject enemyTutorialShip;
    public Sprite enemyImg;
    public GameObject enemySpawnerBL, enemySpawnerTL, enemySpawnerBR, enemySpawnerTR;

    //HOLE
    public Sprite holeImg;

    //static destroyable enemies
    private bool staticShipSpawned;
    public GameObject shipLeft;
    public GameObject shipRight;

    private void Start()
    {
        woodCircle.Stop();
        gunpowderCircle.Stop();
        cannonballCircle.Stop();
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
                Debug.Log("case: " + stage);
                tutorialBubbleInterior.sprite = enemyImg;

                CNanim.SetBool("PlayTutorialBubble", true);

                if (bubbleHasPlayed == true)
                {
                    stage = 1;
                    bubbleHasPlayed = false;
                }
                break;

            case 1:
                Debug.Log("case: " + stage);
                tutorialBubbleInterior.sprite = shipHoldImg;
                CNanim.SetBool("PlayTutorialBubble", true);
                shipHoldAnim.SetBool("PlayTutorialHold", true);
                stage = 2;
                break;

            case 2:
                Debug.Log("case: " + stage);
                if (dpadMenu.alpha > 0.5f)
                {
                    shipHoldAnim.SetBool("PlayTutorialHold", false);

                    //if (CNanim.GetBool("PlayTutorialBubble") == false)
                    //{
                    //    tutorialBubbleInterior.sprite = cannonballImg;
                    //    CNanim.SetBool("PlayTutorialBubble", true);
                    //}


                    if (!cannonballCircle.isPlaying)
                    {
                        cannonballCircle.Play();
                    }

                    foreach (GameObject player in players)
                    {
                        if (player.GetComponent<PlayerStates>().playerState == PlayerStates.PlayerState.pCannonball)
                        {
                            cannonballCircle.Stop();
                            bubbleHasPlayed = false;
                            stage = 3;
                            break;
                        }
                    }
                }
                else
                {

                    cannonballCircle.Stop();
                }
                break;

            case 3:
                Debug.Log("case: " + stage);
                tutorialBubbleInterior.sprite = cannonImg;
                CNanim.SetBool("PlayTutorialBubble", true);
                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    CNanim.SetBool("PlayTutorialBubble", true);
                }
                cannonTopLeftAnim.SetBool("PlayTutorialCannon", true);

                stage = 4;
                break;

            case 4:
                Debug.Log("case: " + stage);
                if (cannonTopLeft.GetComponent<CannonState>().currentState == CannonState.CannonStates.cCannonBall)
                {
                    tutorialBubbleInterior.sprite = shipHoldImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    cannonTopLeftAnim.SetBool("PlayTutorialCannon", false);
                    shipHoldAnim.SetBool("PlayTutorialHold", true);
                    stage = 5;
                }
                break;

            case 5:
                Debug.Log("case: " + stage);
                if (dpadMenu.alpha > 0.5f)
                {
                    shipHoldAnim.SetBool("PlayTutorialHold", false);

                    
                    //if (CNanim.GetBool("PlayTutorialBubble") == false)
                    //{
                    //    tutorialBubbleInterior.sprite = gunpowderImg;
                    //    CNanim.SetBool("PlayTutorialBubble", true);
                    //}


                    if (!gunpowderCircle.isPlaying)
                    {
                        gunpowderCircle.Play();
                    }

                    foreach (GameObject player in players)
                    {
                        if (player.GetComponent<PlayerStates>().playerState == PlayerStates.PlayerState.pGunpowder)
                        {
                            gunpowderCircle.Stop();
                            bubbleHasPlayed = false;
                            stage = 6;
                            break;
                        }
                    }
                }
                else
                {
                    gunpowderCircle.Stop();
                }
                break;

            case 6:
                Debug.Log("case: " + stage);
                tutorialBubbleInterior.sprite = cannonImg;
                CNanim.SetBool("PlayTutorialBubble", true);
                if (bubbleHasPlayed == false)
                {
                    CNanim.SetBool("PlayTutorialBubble", true);
                }
                cannonTopLeftAnim.SetBool("PlayTutorialCannon", true);

                stage = 7;
                break;

            case 7:
                Debug.Log("case: " + stage);
                if (cannonTopLeft.GetComponent<CannonState>().currentState == CannonState.CannonStates.cFullyLoaded)
                {
                    tutorialBubbleInterior.sprite = enemyImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    cannonTopLeftAnim.SetBool("PlayTutorialCannon", false);
                    stage = 8;
                }
                break;

            case 8:
                Debug.Log("case: " + stage);
                Instantiate(enemyTutorialShip, enemySpawnerTL.transform.position, enemySpawnerTL.transform.rotation);
                stage = 9;
                break;

            case 9:
                Debug.Log("case: " + stage);
                tutorialBubbleInterior.sprite = enemyImg;
                if (bubbleHasPlayed == false)
                {
                    CNanim.SetBool("PlayTutorialBubble", true);
                }
                break;

            case 10:
                Debug.Log("case: " + stage);
                tutorialBubbleInterior.sprite = torchImg;
                if (bubbleHasPlayed == false)
                {
                    CNanim.SetBool("PlayTutorialBubble", true);
                    torchAnim.SetBool("PlayTutorialTorch", true);
                    stage = 11;
                }
                break;

            case 11:
                Debug.Log("case: " + stage);
                if (torch.GetComponent<TorchStates>().currentState == TorchStates.TorchState.Held)
                {
                    torchAnim.SetBool("PlayTutorialTorch", false);

                    
                    if (CNanim.GetBool("PlayTutorialBubble") == false)
                    {
                        tutorialBubbleInterior.sprite = cannonImg;
                        CNanim.SetBool("PlayTutorialBubble", true);
                        stage = 12;
                    }
                }
                break;

            case 12:
                Debug.Log("case: " + stage);
                if (cannonTopLeft.GetComponent<CannonState>().currentState == CannonState.CannonStates.cEmpty)
                {
                    if (CNanim.GetBool("PlayTutorialBubble") == false)
                    {
                        if (staticShipSpawned == false)
                        {
                            shipRight = Instantiate(enemyTutorialShip, enemySpawnerBR.transform.position, enemySpawnerBR.transform.rotation);
                            tutorialBubbleInterior.sprite = enemyImg;
                            CNanim.SetBool("PlayTutorialBubble", true);
                            staticShipSpawned = true;
                        }
                    }
                }
                break;

            case 13:
                Debug.Log("case: " + stage);
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    if (CNanim.GetBool("PlayTutorialBubble") == false)
                    {
                        if (staticShipSpawned == true)
                        {
                            shipLeft = Instantiate(enemyTutorialShip, enemySpawnerBL.transform.position, enemySpawnerBL.transform.rotation);
                            tutorialBubbleInterior.sprite = enemyImg;
                            CNanim.SetBool("PlayTutorialBubble", true);
                            staticShipSpawned = false;
                        }
                    }
                }
                break;

            case 14:
                Debug.Log("case: " + stage);
                if (shipRight.activeSelf == false && shipLeft.activeSelf == false)
                {
                    Debug.Log("right and left r ded");
                    tutorialBubbleInterior.sprite = enemyImg;
                    CNanim.SetBool("PlayTutorialBubble", true);

                    Instantiate(enemyTutorialShip, enemySpawnerBL.transform.position, enemySpawnerBL.transform.rotation);

                    timer = 17;
                    stage = 15;
                }
                break;

            case 15:
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    tutorialBubbleInterior.sprite = holeImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    stage = 16;
                }
                break;

            case 16:
                Debug.Log("case: " + stage);
                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    tutorialBubbleInterior.sprite = shipHoldImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    shipHoldAnim.SetBool("PlayTutorialHold", true);
                    stage = 17;
                }
                break;

            case 17:
                Debug.Log("case: " + stage);
                if (dpadMenu.alpha > 0.5f)
                {
                    shipHoldAnim.SetBool("PlayTutorialHold", false);

                    //if (CNanim.GetBool("PlayTutorialBubble") == false)
                    //{
                    //    tutorialBubbleInterior.sprite = woodImg;
                    //    CNanim.SetBool("PlayTutorialBubble", true);
                    //}


                    if (!woodCircle.isPlaying)
                    {
                        woodCircle.Play();
                    }

                    foreach (GameObject player in players)
                    {
                        if (player.GetComponent<PlayerStates>().playerState == PlayerStates.PlayerState.pWood)
                        {
                            woodCircle.Stop();
                            bubbleHasPlayed = false;
                            stage = 18;
                            break;
                        }
                    }
                }
                else
                {

                    woodCircle.Stop();
                }
                break;

            case 18:
                Debug.Log("case: " + stage);
                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    tutorialBubbleInterior.sprite = holeImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    stage = 19;
                }
                break;

            case 19:
                Debug.Log("case: " + stage);
                Debug.Log("case 18 is end of current tutorial.");
                break;
        }
    }

    public void StopTutorialBubble()
    {
        bubbleHasPlayed = true;
        CNanim.SetBool("PlayTutorialBubble", false);
    }
}
