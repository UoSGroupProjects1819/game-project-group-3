using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public float timer;

    [Header("[Task Reference]")]
    public static string highestActivityName;
    public static int highestActivityAmount;
    public static Dictionary<string, int> activeTasks = new Dictionary<string, int>();

    public static void AddTask(string name)
    {
        if (activeTasks.ContainsKey(name))
        {
            activeTasks[name]++;
        }
        else
        {
            activeTasks.Add(name, 1);
        }
    }

    public static void RemoveTask(string name)
    {
        if (activeTasks.ContainsKey(name))
        {
            if (activeTasks[name] <= 1)
            {
                activeTasks[name] = 0;
            }
            else
            {
                activeTasks[name]--;
            }
        }
        else
        {
            Debug.LogError("Task Does Not Exist!");
        }
    }

    public static void FindMostActive()
    {
        highestActivityName = null;
        highestActivityAmount = 0;

        foreach (KeyValuePair<string, int> task in activeTasks)
        {
            if (task.Value > highestActivityAmount)
            {
                highestActivityName = task.Key;              
            }
        }

        Debug.Log("Highest Activity is: " + highestActivityName);
    }
}
