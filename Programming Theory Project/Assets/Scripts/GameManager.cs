using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefab;
    [HideInInspector] public GameObject playerInstance;

    public Animator InstructionAnimation;


    public int playerIndex;

    void Awake()
    {
        InstructionAnimation.SetTrigger("InstructionAnimation");
        if (DataM.instance != null)
        {
            playerIndex = DataM.instance.playerIndex;
            Destroy(DataM.instance.gameObject);
        }
        else
            playerIndex = 0;

        Debug.Log("GameManager Awake: playerIndex = " + playerIndex);
        PlayerSpawn();
    }

    void Update()
    {

    }

    void PlayerSpawn()
    {
        Vector3 position = new Vector3(0, 4f, 0);
        playerInstance = Instantiate(playerPrefab[playerIndex], position, Quaternion.identity);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
