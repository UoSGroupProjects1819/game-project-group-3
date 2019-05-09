using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public bool startFromBeginning;

    public Camera mainCam;
    public LevelManager levelManagerScript;
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
    public float originalTimer;
    public float originalInitialTimer;
    public float tutorialTimer;
    public float tutorialInitialTimer;
    public float tutorialShortInitialTimer;
    public float tutorialShortTimer;
    public Wheel wheel;
    public Sprite wheelImg;
    public Animator wheelAnim;

    //WHALE
    public GameObject whale;
    public Sprite whaleImg;
    public Animator whaleAnim;
    public Animator mastAnim;
    public Sprite mastImg;

    //TICK PICS
    public Sprite enemyTick;
    public Sprite cannonTick;
    public Sprite whaleTick;
    public Sprite rockTick;
    public Sprite seagullTick;    



    private void Start()
    {
        originalMaxHeight = floodController.maxHeight;
        originalFloodRateModifier = floodController.floodRateModifier;
        originalBailAmount = floodController.bailAmount;

        originalInitialTimer = rock.initialTime;
        originalTimer = rock.timer;

        woodCircle.Stop();
        gunpowderCircle.Stop();
        cannonballCircle.Stop();

        leftProjector.gameObject.SetActive(false);
        leftProjector.gameObject.SetActive(false);

        if (startFromBeginning)
            stage = 0;
    }

    void Update()
    {
        TutorialSteps();

        if (Input.GetKeyUp(KeyCode.PageUp))
        {
            stage++;
        }
    }

    void TutorialSteps()
    {
        switch (stage)
        {
            #region cannons
            case 0:
                floodController.isTutorial = true;
                rock.isTutorial = true;
                spawnSeagull.isTutorial = true;
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
                    tutorialBubbleInterior.sprite = enemyImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    Instantiate(enemyTutorialShip, enemySpawnerBL.transform.position, enemySpawnerBL.transform.rotation);
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

                    if (floodController.currentPosition.y >= tutorialMaxHeight.y)
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
                    timer = 5f;
                    stage = 23;
                }
                break;
            #endregion
            #region SECTION #1 completed
            //START/////////////////////////////////////////////////
            case 23:
                Debug.Log("case: " + stage);

                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    tutorialBubbleInterior.sprite = enemyTick;
                    CNanim.SetBool("PlayTutorialBubble", true);

                    stage = 24;
                }
                break;


            case 24:
                Debug.Log("case: " + stage);

                floodController.currentPosition.y = Mathf.Lerp(floodController.currentPosition.y, floodController.startPosition.y, 0.1f * Time.deltaTime);

                if (floodController.currentPosition.y < 7f)
                {
                    timer = 4;
                    stage = 25;
                }
                break;
            //END/////////////////////////////////////////////////
            #endregion

            #region seagull

            case 25:
                Debug.Log("case: " + stage);
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    tutorialBubbleInterior.sprite = seagullImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    spawnSeagull.Spawn();
                    stage = 26;
                }
                break;

            case 26:
                Debug.Log("case: " + stage);
                break;

            case 27:
                Debug.Log("case: " + stage);
                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    tutorialBubbleInterior.sprite = mopImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    mopAnim.SetBool("PlayTutorialMop", true);
                    stage = 28;
                }
                break;

            case 28:
                Debug.Log("case: " + stage);
                if (mop.GetComponent<MopStates>().currentState == MopStates.MopState.Held)
                {
                    mopAnim.SetBool("PlayTutorialMop", false);

                    if (CNanim.GetBool("PlayTutorialBubble") == false)
                    {
                        tutorialBubbleInterior.sprite = pooImg;
                        CNanim.SetBool("PlayTutorialBubble", true);
                        stage = 29;
                    }
                }
                break;

            case 29:
                Debug.Log("case: " + stage);
                if (mop.GetComponent<MopStates>().currentState == MopStates.MopState.Cleaning)
                {
                    stage = 30;
                }
                break;

            case 30:
                Debug.Log("case: " + stage);
                if (mop.GetComponent<MopStates>().currentState == MopStates.MopState.Held)
                {
                    timer = 2;
                    stage = 31;
                }
                break;

            case 31:
                Debug.Log("case: " + stage);
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    tutorialBubbleInterior.sprite = seagullImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    spawnSeagull.Spawn();
                    timer = 1f;
                    stage = 32;
                }
                break;

            case 32:
                Debug.Log("case: " + stage);
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    tutorialBubbleInterior.sprite = seagullImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    spawnSeagull.Spawn();
                    timer = 1f;
                    stage = 33;
                }
                break;

            case 33:
                Debug.Log("case: " + stage);
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    tutorialBubbleInterior.sprite = seagullImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    spawnSeagull.Spawn();
                    timer = 16f;
                    stage = 34;
                }
                break;
            #endregion
            #region SECTION #2 completed
            //START/////////////////////////////////////////////////
            case 34:
                Debug.Log("case: " + stage);
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    if (CNanim.GetBool("PlayTutorialBubble") == false)
                    {
                        tutorialBubbleInterior.sprite = seagullTick;
                        CNanim.SetBool("PlayTutorialBubble", true);

                        stage = 35;
                    }
                }
                break;

            case 35:
                Debug.Log("case: " + stage);

                floodController.currentPosition.y = Mathf.Lerp(floodController.currentPosition.y, floodController.startPosition.y, 0.1f * Time.deltaTime);

                if (floodController.currentPosition.y < 7f)
                {
                    timer = 10;
                    stage = 36;
                }
                break;
            //END/////////////////////////////////////////////////
            #endregion

            #region wheel
            case 36:
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    Debug.Log("case: " + stage);
                    tutorialBubbleInterior.sprite = rockImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    rock.initialTime = tutorialShortInitialTimer;
                    rock.timer = tutorialShortTimer;
                    stage = 37;
                }
                break;

            case 37:
                Debug.Log("case: " + stage);
                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    rock.Spawn();
                    stage = 38;
                }
                break;

            case 38:
                Debug.Log("case: " + stage);
                if (rock.GetComponent<Rocks>().rockStates == Rocks.RockStates.Idle)
                {
                    timer = 2.5f;

                    stage = 39;
                }
                break;

            case 39:
                Debug.Log("case: " + stage);
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    tutorialBubbleInterior.sprite = rockImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    rock.initialTime = tutorialInitialTimer;
                    rock.timer = tutorialTimer;
                    stage = 40;
                }
                break;


            case 40:
                Debug.Log("case: " + stage);
                rock.Spawn();
                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    tutorialBubbleInterior.sprite = wheelImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    wheelAnim.SetBool("PlayTutorialWheel", true);

                    stage = 41;
                }
                break;

            case 41:
                Debug.Log("case: " + stage);
                if (wheel.GetComponent<Wheel>().wheelStates == Wheel.WheelStates.Active)
                {
                    wheelAnim.SetBool("PlayTutorialWheel", false);
                    stage = 42;
                }
                break;

            case 42:
                Debug.Log("case: " + stage);
                if (wheel.GetComponent<Wheel>().wheelStates == Wheel.WheelStates.Exiting)
                {
                    timer = 1f;
                    stage = 43;
                }
                break;
            #endregion
            #region SECTION #3 completed
            //START/////////////////////////////////////////////////
            case 43:
                Debug.Log("case: " + stage);

                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    tutorialBubbleInterior.sprite = rockTick;
                    CNanim.SetBool("PlayTutorialBubble", true);

                    stage = 44;
                }
                break;

            case 44:
                Debug.Log("case: " + stage);

                floodController.currentPosition.y = Mathf.Lerp(floodController.currentPosition.y, floodController.startPosition.y, 0.1f * Time.deltaTime);

                if (floodController.currentPosition.y < 7f)
                {
                    timer = 2;
                    stage = 45;
                }
                break;
            //END/////////////////////////////////////////////////
            #endregion

            #region whale
            case 45:
                Debug.Log("case: " + stage);
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    tutorialBubbleInterior.sprite = whaleImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    stage = 46;
                }
                break;

            case 46:
                Debug.Log("case: " + stage);
                whale.SetActive(true);
                timer = 10f;
                stage = 47;
                break;

            case 47:
                Debug.Log("case: " + stage);
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    tutorialBubbleInterior.sprite = whaleImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    stage = 48;
                }
                break;

            case 48:
                Debug.Log("case: " + stage);
                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    tutorialBubbleInterior.sprite = mastImg;
                    CNanim.SetBool("PlayTutorialBubble", true);
                    mastAnim.SetBool("PlayTutorialMast", true);
                    stage = 49;
                }
                break;

            case 49:
                Debug.Log("case: " + stage);

                if (players[0].GetComponent<PlayerStates>().playerState == PlayerStates.PlayerState.pHoldingOn && players[1].GetComponent<PlayerStates>().playerState == PlayerStates.PlayerState.pHoldingOn)
                {
                    mastAnim.SetBool("PlayTutorialMast", false);
                    Debug.Log("both players are holding on");
                    stage = 50;
                }

                break;

            case 50:
                Debug.Log("case: " + stage);
                whale.SetActive(true);
                stage = 51;
                break;

            case 51:
                Debug.Log("case: " + stage);
                if (whale.GetComponent<Whale>().whaleStates == Whale.WhaleStates.exiting)
                {
                    whale.SetActive(false);
                    timer = 1f;
                    stage = 52;
                }
                break;
            #endregion
            #region SECTION #4 completed
            //START/////////////////////////////////////////////////
            case 52:
                Debug.Log("case: " + stage);

                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    tutorialBubbleInterior.sprite = whaleTick;
                    CNanim.SetBool("PlayTutorialBubble", true);

                    timer = 3;

                    stage = 53;
                }
                break;

            case 53:
                Debug.Log("case: " + stage);

                if (CNanim.GetBool("PlayTutorialBubble") == false)
                {
                    timer -= Time.deltaTime;
                    if (timer < 0)
                    {
                        floodController.currentPosition.y = Mathf.Lerp(floodController.currentPosition.y, floodController.startPosition.y, 0.1f * Time.deltaTime);

                        if (floodController.currentPosition.y < 7f)
                        {
                            stage = 998;
                        }
                    }
                }
                break;
            //END/////////////////////////////////////////////////
            #endregion

            #region end tutorial, reassign initial flood manager defaults

            case 998:
                Debug.Log("case: " + stage + ". Flood controller values reset.");
                floodController.maxHeight = originalMaxHeight;
                floodController.floodRateModifier = originalFloodRateModifier;
                floodController.bailAmount = originalBailAmount;
                floodController.isTutorial = false;

                Debug.Log("case: " + stage + ". Rock controller values reset.");
                rock.initialTime = originalInitialTimer;
                rock.timer = originalTimer;
                rock.isTutorial = false;

                Debug.Log("case: " + stage + ". Seagull controller values reset.");
                spawnSeagull.isTutorial = false;

                stage = 999;
                break;

            case 999:
                Debug.Log("case: " + stage + ". THE TUTORIAL IS WON! ONWARD TO THE HIGH SEAS!");
                mainCam.transform.position += new Vector3(0, 0, 0.4f);

                if (mainCam.transform.position.z > 100)
                {
                    levelManagerScript.FadeToLevel(0);
                }
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
