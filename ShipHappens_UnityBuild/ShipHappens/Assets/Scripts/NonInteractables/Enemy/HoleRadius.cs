using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleRadius : MonoBehaviour
{
    public enum HoleStates { Impact, Open, Repaired, Dormant };
    public HoleStates holeStates;

    //public ParticleSystem impactPS;

    void Update()
    {
        HoleStatus();
    }

    void HoleStatus()
    {
        switch(holeStates)
        {
            case HoleStates.Impact: //set when first hitting deck, set if ball impacts within radius when dormant, increments flood manager
                //impactPS.transform.position = transform.position;
                transform.rotation = Quaternion.identity;
                //impactPS.Play();
                FloodController.numberOfHoles++;
                holeStates = HoleStates.Open;
                break;

            case HoleStates.Open: //waiting for next input
                break;

            case HoleStates.Repaired: //set when player repairs, amends flood manager, sets self to dormant
                Vector3 rot = transform.rotation.eulerAngles;
                rot = new Vector3(rot.x + 180, rot.y, rot.z);
                transform.rotation = Quaternion.Euler(rot);
                FloodController.numberOfHoles--;
                holeStates = HoleStates.Dormant;
                break;

            case HoleStates.Dormant: //waiting for next input
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DropBall")
        {
            holeStates = HoleStates.Impact;
        }
    }
}
