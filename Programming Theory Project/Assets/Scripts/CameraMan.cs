using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    public GameManager gameManager;

    private Vector3 offset = new Vector3(-0.28f, 1.4f, -2.7f);

    private void Start()
    {
        StartCoroutine(FollowPlayer());
    }

    private IEnumerator FollowPlayer()
    {
        while (gameManager.playerInstance == null)
        {
            yield return null;  // Attendez le prochain frame
        }

        while (true)
        {
            transform.position = gameManager.playerInstance.transform.position + offset;
            yield return null;  // Attendez le prochain frame
        }
    }
}
