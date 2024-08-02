using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataM : MonoBehaviour
{
    public static DataM instance;

    public int playerIndex;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("DataM Start: playerIndex = " + playerIndex);
    }
}
