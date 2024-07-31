using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUiHandel : MonoBehaviour
{

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    private void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}
