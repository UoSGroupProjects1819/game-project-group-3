using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // keep track of # of active events & type of active events
    // timer to trigger events randomly
    // keep active events above minimum amount //minimum amount = no. of players + x
    // ceiling curve for active events //if (many events active) extend timer?

    public string Abutton = "A_P1";
    
    [Header("[Countdown]")]
    public float timer;
    public float lowRand, highRand;

    [Header("[Task References]")]
    public GameObject[] NonInteractablePrefabs;
    public List<string> activeTasks = new List<string>();
    public List<KeyValuePair<int, int>> TestList = new List<KeyValuePair<int, int>>();

    [Header("[Deck Flooding]")]   
    public Vector3 floodStartPosition = new Vector3(-3.863f, 4.0f, 6.85f);
    public float floodLevel;
    private GameObject floodPlane;

    [Header("[Hold Timers]")]
    public DpadBarrelTimer barrelTimer;
    public DpadWoodTimer woodTimer;
    public DpadCannonballTimer cballTimer;





    //singleton instance
    public static GameManager Instance { get; private set; }

    //preventing multiple instances
	private void Awake ()
    {
		if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    void Start()
    {
        floodPlane = GameObject.FindGameObjectWithTag("FloodWater");

        floodPlane.transform.position = floodStartPosition;

        TestList.Insert(0, new KeyValuePair<int, int>(1, 1));

    }

    void Update ()
    {
        Timer();

        UpdateFlood();
	}

    //set flood height
    void UpdateFlood()
    {
        if (floodPlane.transform.position.y < 4)
        {
            floodPlane.transform.position = floodStartPosition;
        }

        floodPlane.transform.position = new Vector3(floodStartPosition.x, floodLevel + 4f, floodStartPosition.z);
    }

    void Timer()
    {
        timer -= Time.deltaTime;

        Dictionary<string, int> tempDictionary = new Dictionary<string, int>(); //create dictionary, used to check active tasks
        string mostCommon = activeTasks[0]; //hold most occuring
        tempDictionary.Add(activeTasks[0], 1); //0th item added to dictionary or for loop cannot run

        List<KeyValuePair<GameObject, int>> KVlist = new List<KeyValuePair<GameObject, int>>();


        
        

        if (timer < 0)
        {
            for (int i = 0; i < activeTasks.Count; i++) //loop through active task list
            {
                if (tempDictionary.ContainsKey(activeTasks[i])) //if dictionary already contains key, move to next
                {
                    tempDictionary[activeTasks[i]] += 1;

                    if (tempDictionary[activeTasks[i]] > tempDictionary[mostCommon]) 
                    {
                        mostCommon = activeTasks[i];
                    }
                }
                else
                {
                    tempDictionary.Add(activeTasks[i], 1);
                }
            }
            Debug.Log(mostCommon);


                //check list of actives
                //instantiate prefab of task with least count
                //set timer to new value depending on how many active tasks

                timer = Random.Range(lowRand, highRand);
        }
    }
}
