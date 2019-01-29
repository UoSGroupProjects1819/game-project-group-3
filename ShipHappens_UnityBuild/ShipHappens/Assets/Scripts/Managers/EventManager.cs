using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    // Dictionary to hold active tasks.  Key - String = Activity name.  Value - Int = Amount of Active Events
    public static Dictionary<string, int> activeTasks = new Dictionary<string, int>();

    public static void AddTask(string name)
    {
        //Checks if task already exists, if it doesn't it adds a new Key into the dictionary
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
            // Decrement the amount of tasks active, without allowing the amount to go below 0
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

    public static string FindMostActive()
    {
        string highestActivityName = null;
        int highestActivityAmount = 0;

        foreach (KeyValuePair<string, int> task in activeTasks)
        {
            if (task.Value > highestActivityAmount)
            {
                highestActivityName = task.Key;              
            }
        }

        Debug.Log("Highest Activity is: " + highestActivityName);
        return highestActivityName;
    }

    public static bool IsActive(string name)
    {
        if (activeTasks.ContainsKey(name))
        {
            if (activeTasks[name] >= 1)
            { return true; }
            else
            { return false; }
        }
        else
        { return false; }
    }

    public static int ActiveAmount(string name)
    {
        if (activeTasks.ContainsKey(name))
        { return activeTasks[name]; }
        else
        { return 0; }
        
    }
}