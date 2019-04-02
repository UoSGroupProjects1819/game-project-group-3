using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiObjectPool : MonoBehaviour
{
    [System.Serializable]//(shows class in inspector)
    public class Pool
    {
        public string poolName;
        public GameObject poolPrefab;
        public int maxPoolSize;
    }

    #region Singleton
    //singleton instance
    public static MultiObjectPool Instance { get; private set; }
    //preventing multiple instances
    private void Awake()
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
    #endregion

    public List<Pool> pools;
    //public GameObject prefab;

    [SerializeField]
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    

	void Start ()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool ipool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < ipool.maxPoolSize; i++)
            {
                GameObject ipoolObj = Instantiate(ipool.poolPrefab);
                ipoolObj.SetActive(false);

                objectPool.Enqueue(ipoolObj);
            }


            poolDictionary.Add(ipool.poolName, objectPool);
        }
	}
	
    public GameObject SpawnFromPool (string name, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(name))
        {
            Debug.LogWarning("Pool with tag: " + name + " does not exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[name].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[name].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}