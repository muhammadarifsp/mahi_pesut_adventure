using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StoryMode : MonoBehaviour
{
    private string gameMode = "StoryMode";
    private int lastStage;
    public GameObject continueBtn;
    public GameObject fadeOut;
    public TMP_Text stage;

    private void Start()
    {
        lastStage = PlayerPrefs.GetInt("stage", 1);
        if (PlayerPrefs.HasKey("stage"))
        {
            continueBtn.GetComponent<Button>().interactable = true;
        }

        stage.text = "Stage " + lastStage;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void StartGame(string type)
    {
        StartCoroutine(StartingGame(type));
    }

    IEnumerator StartingGame(string type)
    {
        fadeOut.SetActive(true);
        PlayerPrefs.SetString("GameMode", gameMode);
        if (type == "New Game")
            PlayerPrefs.SetInt("stage", 1);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("FreeRun(Play)");
    }
}
