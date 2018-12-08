using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftBubbleEnd : MonoBehaviour
{
    public Animator leftAnimator;
    public CrowsNestUI CNui;

    void Start()
    {
        leftAnimator = GetComponent<Animator>();
    }


    void StopLeftBubbleAnim()
    {
        CNui.playLeft = false;
        leftAnimator.SetBool("PlayLeft", false);
    }
}
