using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGame : MonoBehaviour
{
    private string gameMode;
    private int lastStage;
    private int goal;
    public GameObject gameManager;

    private void Awake()
    {
        lastStage = PlayerPrefs.GetInt("stage", 1);
        gameMode = PlayerPrefs.GetString("GameMode");

        if (gameMode == "StoryMode")
        {
            // mode story
            goal = lastStage * 50;
        }
        else if (gameMode == "FreeRun")
        {
            // mode free run
        }
    }

    private void Update()
    {
        // story mode
        if (gameMode == "StoryMode")
        {
            int score = ScoreController.scoreCount;
            if (score >= goal)
            {
                gameManager.GetComponent<StageFinished>().enabled = true; // enable script for stage finished
            }
        }
    }
}
