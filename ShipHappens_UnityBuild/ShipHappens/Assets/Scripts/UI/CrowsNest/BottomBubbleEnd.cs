using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBubbleEnd : MonoBehaviour
{
    private Animator bottomAnimator;
    public CrowsNestUI CNui;

    public ScreenShake shake;
    public GameObject water;

    void Start ()
    {
        bottomAnimator = GetComponent<Animator>();
	}
	

	void StopBottomBubbleAnim()
    {
        CNui.playBottom = false;
        bottomAnimator.SetBool("PlayBottom", false);
    }

    //void MoveWater()
    //{
    //    shake.mediumShake = true;
    //    shake.shouldShake = true;
    //    Vector3 currentWaterPos = water.transform.position;
    //    Vector3 nextWaterPos;
    //    nextWaterPos.x = currentWaterPos.x;
    //    nextWaterPos.z = currentWaterPos.z;
    //    nextWaterPos.y = 10f;

    //    water.transform.position = nextWaterPos;
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.B))
    //    {
    //        Debug.Log("ye");
    //        Vector3 currentWaterPos = water.transform.position;
    //        Vector3 nextWaterPos;
    //        nextWaterPos.x = currentWaterPos.x;
    //        nextWaterPos.z = currentWaterPos.z;
    //        nextWaterPos.y = currentWaterPos.y - 0.25f;

    //        water.transform.position = nextWaterPos;
    //    }
    //}
}
