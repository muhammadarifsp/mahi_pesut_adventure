using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject fadeOut;

    public void StartButton()
    {
        StartCoroutine(StartingGame());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator StartingGame()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("FreeRun(Play)");
    }
}
