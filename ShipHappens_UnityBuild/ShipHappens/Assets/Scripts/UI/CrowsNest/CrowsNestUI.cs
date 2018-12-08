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

        if (playLeft)
            leftAnim.SetBool("PlayLeft", true);

        if (playRight)
            rightAnim.SetBool("PlayRight", true);
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
}
