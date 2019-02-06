using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
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
    #endregion

    private void Update()
    {
        
    }


}
