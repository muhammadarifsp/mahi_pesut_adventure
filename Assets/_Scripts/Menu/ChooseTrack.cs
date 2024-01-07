using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ChooseTrack : MonoBehaviour
{
    public GameObject[] listTrack;
    public GameObject fadeOut;
    public Button startButton;
    public TMP_Text totalScoreText;
    public GameObject trackLocked;
    public GameObject trackUnlocked;
    public GameObject panelUnlock;
    public GameObject panelInfo;
    public GameObject panelCongrats;
    private string trackName;
    private bool trackOpened;
    private int totalScore;
    public int goalScore = 500;

    void Awake()
    {
        trackName = "";
        trackOpened = false;
        if (PlayerPrefs.HasKey("TrackOpen"))
        {
            trackOpened = PlayerPrefs.GetInt("TrackOpen") == 1 ? true : false;
            trackLocked.SetActive(false);
            trackUnlocked.SetActive(true);
        }
        else if (PlayerPrefs.HasKey("TotalScore"))
        {
            totalScore = PlayerPrefs.GetInt("TotalScore");
            totalScoreText.text = totalScore + "/500";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Choose(GameObject track)
    {
        Debug.Log(track.transform.name + " choosed.");
        trackName = track.transform.name;
        EnableTrackByIndex(track.transform.name);
    }

    private void EnableTrackByIndex(string track)
    {
        if (track == "Village")
        {
            startButton.interactable = true;
            listTrack[0].transform.GetComponent<Outline>().enabled = true;
            listTrack[1].transform.GetComponent<Outline>().enabled = false;
        }
        else if (track == "City" && trackOpened)
        {
            startButton.interactable = true;
            listTrack[0].transform.GetComponent<Outline>().enabled = false;
            listTrack[1].transform.GetComponent<Outline>().enabled = true;
        }
    }

    public void startGame()
    {
        PlayerPrefs.SetString("Track", trackName);
        PlayerPrefs.SetString("GameMode", "FreeRun");
        StartCoroutine(StartingGame());
    }

    IEnumerator StartingGame()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("FreeRun(Play)");
    }

    public void OpenLockedTrack()
    {
        if (!trackOpened)
        {
            if (totalScore >= goalScore)
                panelUnlock.SetActive(true);
            else
                panelInfo.SetActive(true);
        }
    }

    public void ConfirmUnlock()
    {
        if (totalScore >= goalScore)
        {
            trackOpened = true;
            PlayerPrefs.SetInt("TrackOpen", trackOpened ? 1 : 0);
            trackLocked.SetActive(false);
            trackUnlocked.SetActive(true);
            ClosePanel();
            panelCongrats.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        panelUnlock.SetActive(false);
        panelInfo.SetActive(false);
    }
}
