using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightBubbleEnd : MonoBehaviour
{
    private Animator rightAnimator;
    public CrowsNestUI CNui;

    void Start()
    {
        rightAnimator = GetComponent<Animator>();
    }


    void StopRightBubbleAnim()
    {
        CNui.playRight = false;
        rightAnimator.SetBool("PlayRight", false);
        CNui.is3active = false;
    }
}
