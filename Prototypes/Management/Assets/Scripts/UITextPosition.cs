using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextPosition : MonoBehaviour
{
    public GameObject player;
    public Text text;

	void Update ()
    {
        Vector3 UIPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        text.transform.position = UIPosition;
	}
}
