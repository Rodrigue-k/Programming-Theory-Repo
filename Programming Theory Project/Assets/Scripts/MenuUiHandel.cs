using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUiHandel : MonoBehaviour
{
    public GameObject SelectBall;
    private int currentIndex = 0;
    private float anglePerBall = 90f;
    private Coroutine rotationCoroutine;

    [SerializeField] private Image selectImage;

    public void TurnSelectBallR()
    {
        currentIndex++;
        RotateCylinder();
        selectImage.enabled = false;
    }

    public void TurnSelectBallL()
    {
        currentIndex--;
        RotateCylinder();
        selectImage.enabled = false;
    }

    private void RotateCylinder()
    {
        float targetAngle = currentIndex * anglePerBall;
        if (rotationCoroutine != null)
            StopCoroutine(rotationCoroutine);
        rotationCoroutine = StartCoroutine(RotateToAngle(targetAngle));
    }

    private IEnumerator RotateToAngle(float targetAngle)
    {
        float currentAngle = SelectBall.transform.eulerAngles.y;
        float duration = 0.5f; // Durée de la rotation
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float angle = Mathf.Lerp(currentAngle, targetAngle, elapsed / duration);
            SelectBall.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            yield return null;
        }

        SelectBall.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

    public void SelectedBall()
    {
        int positiveIndex = (currentIndex % 4 + 4) % 4; // Pour gérer les indices négatifs correctement
        DataM.instance.playerIndex = positiveIndex;
        selectImage.enabled = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
