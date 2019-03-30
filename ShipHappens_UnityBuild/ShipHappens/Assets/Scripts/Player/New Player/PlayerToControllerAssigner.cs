using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerToControllerAssigner : MonoBehaviour
{
    private List<int> assignedControllers = new List<int>();
    private PlayerPanel[] playerPanels;

    private void Awake()
    {
        //playerPanels = FindObjectsOfType<PlayerPanel>().OrderBy(t => t.).ToArray();
    }

    private void Update()
    {
        for (int i = 1; i < 6; i++)
        {
            if (assignedControllers.Contains(i))
                continue;

            //if(Input.GetButton("" ))
        }
    }
}
