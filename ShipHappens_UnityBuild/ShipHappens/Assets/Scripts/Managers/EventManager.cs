using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EventDetails
{
    public string name;
    public int weight;
    public float modifier;
    public Event spawner;   
}

public class EventManager : MonoBehaviour
{
    SpawnSeagull spawnSeagull;
    PirateSpawner pirateSpawner;
    Rocks rocks;

    // Dictionary to hold active tasks.  Key - String = Activity name.  Value - Int = Amount of Active Events
    public Dictionary<string, int> activeTasks = new Dictionary<string, int>();

    public List<EventDetails> nextEvent = new List<EventDetails>();

    private float initialTimer = 5;
    public float timer = 1;

    private void Update()
    {
        CountDown();
    }

    void CountDown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            PickEvent();
            timer = initialTimer;
        }   
    }

    void PickEvent()
    {
        // Do Event stuff
        int maxAmount = 0;
        foreach (var evt in nextEvent)
        {
            maxAmount += evt.weight;
        }

        float rand = Random.Range(0, maxAmount);
        int i = 0;

        foreach (var evt in nextEvent)
        {
            if (IsActive("Whale"))
            {
                PickEvent();
                return;
            }

            if (IsActive("Rock"))
            {
                PickEvent();
                return;
            }

            if (ActiveAmount("Enemy") > 4)
            {
                PickEvent();
                return;
            }

            i += evt.weight;
            if (i >= rand)
            {
                Debug.Log("Event " + evt.name + " Picked");
                evt.spawner.Spawn();
                AddTask(evt.name);
                return;  
            }
        }
    }


    // List of events with weights:
    // sea gulls - 45
    // cannon - 25
    // rock - 15
    // whale - 15

    // gulls, cannon, whale - max: 75

    /*
     * 
     * for (Event evt in Events) {
     *      if (evt.active == false)
     *          activable_events.Add(evt, total)
     *          total += evt.weight
     *          
     * }
     * 
     * rand = Random.Range(0, total)
     * 
     * for ( ActiveEvent evt in activable_events) {
     *      if ( rand < 
     * }
     * 
     * 
     */



    #region Dictionary Functions
    private void AddTask(string name)
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

    public void RemoveTask(string name)
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

    private string FindMostActive()
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

    private bool IsActive(string name)
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

    private int ActiveAmount(string name)
    {
        if (activeTasks.ContainsKey(name))
        { return activeTasks[name]; }
        else
        { return 0; }     
    }
    #endregion
}