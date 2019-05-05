﻿using System.Collections;
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

    //FLOOD CONTROLLER
    public FloodController floodController;
        //original flood values
    public Vector3 originalMaxHeight;
    public float originalFloodRateModifier;
    public float originalBailAmount;
        //tutorial flood values
    public Vector3 tutorialMaxHeight = new Vector3(-3.863f, 8.5f, 6.85f);
    public float tutorialFFloodRateModifier = 40f;
    public float tutorialBailAmount = 5f;

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

    //BUCKET
    public GameObject bucket;
    public Animator bucketAnim;
    public Sprite bucketImg;
    public Animator bailAreaAnim;
    public Projector leftProjector;
    public Projector rightProjector;

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

    //SEAGULL
    public SpawnSeagull spawnSeagull;
    public Sprite seagullImg;

    //MOP
    public GameObject mop;
    public Animator mopAnim;
    public Sprite mopImg;

    //POO
    public Sprite pooImg;

    //WHEEL / ROCK
    public Rocks rock;
    public Sprite rockImg;
    public Wheel wheel;
    public Sprite wheelImg;
    public Animator wheelAnim;

    //WHALE
    public Sprite whaleImg;
    public Animator whaleAnim;
    public Animator mastAnim;




    private void Start()
    {
        originalMaxHeight = floodController.maxHeight;
        originalFloodRateModifier = floodController.floodRateModifier;
        originalBailAmount = floodController.bailAmount;

        woodCircle.Stop();
        gunpowderCircle.Stop();
        cannonballCircle.Stop();

        leftProjector.gameObject.SetActive(false);
        leftProjector.gameObject.SetActive(false);
    }

    void Update()
    {
        TutorialSteps();
    }

    void TutorialSteps()
    {
        switch (stage)
        {
            #region cannons
            case 0:
                floodController.isTutorial = true;
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

                    //timer = 17;
                    stage = 15;
                }
                break;
            #endregion
            #region repair
            case 15:
                Debug.Log("case: " + stage);

                if (FloodController.numberOfHoles > 0)
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
                    floodController.maxHeight = tutorialMaxHeight;
                    floodController.floodRateModifier = tutorialFFloodRateModifier;
                    floodController.bailAmount = tutorialBailAmount;

                    if (floodController.currentPosition.y > tutorialMaxHeight.y)
                    {
                        stage = 17;
                    }
                }
                break;

            case 17:
                Debug.Log("case: " + stage);

                tutorialBubbleInterior.sprite = shipHoldImg;
                CNanim.SetBool("PlayTutorialBubble", true);
                shipHoldAnim.SetBool("PlayTutorialHold", true);
                stage = 18;
                break;

            case 18:
                if (dpadMenu.alpha > 0.5f)
                {
                    shipHoldAnim.SetBool("PlayTutorialHold", false);

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
                            stage = 19;
                            break;
                        }
                    }
                }
                else
                {
                    woodCircle.Stop();
                }
                break;

            case 19:
                Debug.Log("case: " + stage);
                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    tutorialBubbleInterior.sprite = holeImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    stage = 20;
                }
                break;
            #endregion
            #region bail
            case 20:
                Debug.Log("case: " + stage);

                if (FloodController.numberOfHoles == 0)
                {
                    tutorialBubbleInterior.sprite = bucketImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    bucketAnim.SetBool("PlayTutorialBucket", true);
                    stage = 21;
                }
                break;

            case 21:
                Debug.Log("case: " + stage);
                if (bucket.GetComponent<BucketStates>().currentState == BucketStates.BucketState.Held)
                {
                    bucketAnim.SetBool("PlayTutorialBucket", false);

                    leftProjector.gameObject.SetActive(true);
                    leftProjector.gameObject.SetActive(true);
                    bailAreaAnim.SetBool("PlayTutorialBailArea", true);
                    stage = 22;
                }


                break;

            case 22:
                Debug.Log("case: " + stage);
                if (bucket.GetComponent<BucketStates>().currentState == BucketStates.BucketState.Bailing)
                {
                    bailAreaAnim.SetBool("PlayTutorialBailArea", false);
                    leftProjector.gameObject.SetActive(false);
                    leftProjector.gameObject.SetActive(false);
                    timer = 2;
                    stage = 23;
                }
                break;
            #endregion
            #region GAME MANAGER FREEPLAY #1 (including above)
            //START THE MANAGER/////////////////////////////////////////////////
            case 23:
                Debug.Log("case: " + stage);
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    //TOGGLE MANAGER ON
                    timer = 50;
                    stage = 24;
                }
                break;


            case 24:
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    //TOGGLE MANAGER OFF
                    stage = 25;
                }
                break;
            //END THE MANAGER/////////////////////////////////////////////////
            #endregion

            #region seagull

            case 25:
                tutorialBubbleInterior.sprite = seagullImg;
                CNanim.SetBool("PlayTutorialBubble", true);
                spawnSeagull.Spawn();
                break;

            case 26:
                tutorialBubbleInterior.sprite = mopImg;
                CNanim.SetBool("PlayTutorialBubble", true);
                mopAnim.SetBool("PlayTutorialMop", true);
                break;

            case 27:
                if (torch.GetComponent<MopStates>().currentState == MopStates.MopState.Held)
                {
                    mopAnim.SetBool("PlayTutorialMop", false);

                    if (CNanim.GetBool("PlayTutorialBubble") == false)
                    {
                        tutorialBubbleInterior.sprite = pooImg;
                        CNanim.SetBool("PlayTutorialBubble", true);
                        stage = 28;
                    }
                }
                break;

            case 28:
                if (torch.GetComponent<MopStates>().currentState == MopStates.MopState.Cleaning)
                {
                    stage = 29;
                }
                break;

            case 29:
                if (torch.GetComponent<MopStates>().currentState == MopStates.MopState.Held)
                {
                    timer = 2;
                    stage = 30;
                }
                break;

            case 30:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    tutorialBubbleInterior.sprite = seagullImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    spawnSeagull.Spawn();
                    timer = 2.5f;
                    stage = 31;
                }
                break;

            case 31:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    tutorialBubbleInterior.sprite = seagullImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    spawnSeagull.Spawn();
                    timer = 2.5f;
                    stage = 32;
                }
                break;

            case 32:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    tutorialBubbleInterior.sprite = seagullImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    spawnSeagull.Spawn();
                    stage = 33;
                }
                break;
            #endregion
            #region GAME MANAGER FREEPLAY #2 (including above)
            //START THE MANAGER/////////////////////////////////////////////////
            case 33:
                Debug.Log("case: " + stage);
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    //TOGGLE MANAGER ON
                    timer = 50;
                    stage = 34;
                }
                break;


            case 34:
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    //TOGGLE MANAGER OFF
                    stage = 35;
                }
                break;
            //END THE MANAGER/////////////////////////////////////////////////
            #endregion

            #region wheel
            case 35:
                tutorialBubbleInterior.sprite = rockImg;
                CNanim.SetBool("PlayTutorialBubble", true);
                stage = 36;
                break;

            case 36:

                break;
            #endregion
            #region GAME MANAGER FREEPLAY #3(including above)
            #endregion

            #region whale
            #endregion
            #region GAME MANAGER FREEPLAY #4 (including above)
            #endregion

            #region end tutorial, reassign initial flood manager defaults

            case 999:
                Debug.Log("case: " + stage + ". Flood controller values reset.");
                floodController.maxHeight = originalMaxHeight;
                floodController.floodRateModifier = originalFloodRateModifier;
                floodController.bailAmount = originalBailAmount;
                floodController.isTutorial = false;
                break;

            #endregion
        }
    }

    public void StopTutorialBubble()
    {
        bubbleHasPlayed = true;
        CNanim.SetBool("PlayTutorialBubble", false);
    }
}
