using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    public enum WhaleStates { entering, active, exiting };
    public WhaleStates whaleStates;

    public GameObject whale;
    public ParticleSystem whalePS;
    public Animator anim;
    public Camera cam;
    public ScreenShake screenShake;

    public GameObject[] players;

	void Start ()
    {
        screenShake = cam.GetComponent<ScreenShake>();
	}

    void Update()
    {
        switch (whaleStates)
        {
            case WhaleStates.entering:
                //add count to game manager
                //UI update
                //Play audio
                whaleStates = WhaleStates.active;
                break;
            case WhaleStates.active:
                anim.SetBool("PlaySplash", true);
                break;
            case WhaleStates.exiting:
                anim.SetBool("PlaySplash", false);
                break;
        }
    }


    public void PlayWhaleParticles() //remember during testing - if players array is not correct size, for loop will not complete and whale will get stuck in active state
    {
        screenShake.mediumShake = true;
        screenShake.shouldShake = true;

        whalePS.Play();

        for (int i = 0; i < players.Length; i++)
        {
            float randomForceXZ = Random.Range(10, 15);
            float randomForceY = Random.Range(12, 17);
            float randomTorque = Random.Range(100, 666);

            if (players[i].GetComponent<PlayerStates>().playerState == PlayerStates.PlayerState.pHoldingOn)
            {
                //you are holding on, you are safe
                continue;
            }
            else
            {
                //you are not holding on, you gonna dieeeee
                players[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                players[i].GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(randomForceXZ, randomForceY, randomForceXZ), players[i].transform.position, ForceMode.Impulse);
                players[i].GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(500, 225, 600));
            }
        }

        whaleStates = WhaleStates.exiting;
    }
}
