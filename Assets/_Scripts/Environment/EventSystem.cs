using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    public GameObject fadeOut;
    private int previousScene;

    public void ChangeMenu(string SceneName)
    {
        // Debug.Log("Menu " + SceneName + " Clicked");
        SceneManager.LoadScene(SceneName);
    }

    public void Restart()
    {
        StartCoroutine(RestartSequence());
    }

    public void BackToHome()
    {
        StartCoroutine(BackHome());
    }

    IEnumerator RestartSequence()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator BackHome()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }

    public void NextStage()
    {
        StartCoroutine(NextStageSequence());
    }

    IEnumerator NextStageSequence()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
