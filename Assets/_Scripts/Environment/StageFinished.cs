using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageFinished : MonoBehaviour
{
    public GameObject displayPanel,
        stageFinishedPanel,
        fadeOut;
    public GameObject player;
    public GameObject charModel;
    public AudioSource finishFX;
    public GameObject nextStageBtn;
    public TMP_Text titleStageFinishedPanel;
    private int lastStage;

    void Start()
    {
        lastStage = PlayerPrefs.GetInt("stage", 1);

        StartCoroutine(FinishSequence());

        PlayerController.isStarted = false; // reset is the game started

        player.GetComponent<PlayerController>().enabled = false; // disabled script
        ScoreController.isEnded = true; // set bool for stop counting score
        finishFX.Play(); // play sfx for victory
        TileGenerator.isEnded = true; // stop generate tile

        titleStageFinishedPanel.text = "Stage " + lastStage.ToString() + " Finished";
        if (lastStage != 5)
        {
            PlayerPrefs.SetInt("stage", lastStage + 1);
        }
        else
        {
            nextStageBtn.SetActive(false);
        }
    }

    IEnumerator FinishSequence()
    {
        yield return new WaitForSeconds(3);

        displayPanel.SetActive(false);
        stageFinishedPanel.SetActive(true);
    }
}
