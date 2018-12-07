using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowsNestUI : MonoBehaviour
{
    private Animator leftAnim;
    private Animator rightAnim;
    private Animator bottomAnim;

    public Image leftBubble, leftBubbleContents;
    public Image bottomBubble, bottomBubbleContents;
    public Image rightBubble, rightBubbleContents;

    public int numberActive;

    public Image nextAvailableBubble;

    public bool playBottom;
    public bool playLeft;
    public bool playRight;


    private void Start()
    {
        leftAnim = leftBubble.GetComponent<Animator>();
        rightAnim = rightBubble.GetComponent<Animator>();
        bottomAnim = bottomBubble.GetComponent<Animator>();
    }

    void Update ()
    {
        UpdateBubbles();

        /////////////////////////////////

        if (playBottom)
            bottomAnim.SetBool("PlayBottom", true);
        else
            bottomAnim.SetBool("PlayBottom", false);

        if (playLeft)
            bottomAnim.SetBool("PlayLeft", true);
        else
            bottomAnim.SetBool("PlayLeft", false);

        if (playRight)
            bottomAnim.SetBool("PlayRight", true);
        else
            bottomAnim.SetBool("PlayRight", false);
    }

    void UpdateBubbles()
    {
        switch (numberActive)
        {
            case 1:
                nextAvailableBubble = leftBubble;
                break;
            case 2:
                nextAvailableBubble = rightBubble;
                break;
            case 3:
                Debug.Log("godaaaaam u unlucky");
                break;

            default:
                nextAvailableBubble = bottomBubble;
                break;
        }
    }

    //void StopBottomBubble()
    //{
    //    bottomAnim.SetBool("PlayBottom", false);
    //}

    //public void StopLeftBubble()
    //{
    //    bottomAnim.SetBool("PlayLeft", false);
    //}

    //void StopRightBubble()
    //{
    //    bottomAnim.SetBool("PlayRight", false);
    //}
}
