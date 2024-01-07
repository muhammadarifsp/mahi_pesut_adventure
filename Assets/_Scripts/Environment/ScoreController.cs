using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public static int scoreCount;
    public TMP_Text scoreCountDisplay;
    public GameObject scoreEndDisplay,
        scoreStageFinished;
    public bool addingScore = false;
    public float scoreDelay = 0.75f;
    public static bool isEnded = false;
    private string gameMode;
    private int goal;
    private int lastStage;
    private string goalDisplay = "";
    public static bool isBoosted;

    private void Awake()
    {
        isEnded = false;
        isBoosted = false;
        scoreCount = 0;
    }

    private void Start()
    {
        lastStage = PlayerPrefs.GetInt("stage", 1);
        gameMode = PlayerPrefs.GetString("GameMode");

        if (gameMode == "StoryMode")
        {
            goal = lastStage * 50;
            goalDisplay = "/" + goal;
        }
    }

    void Update()
    {
        if (!addingScore && !isEnded)
        {
            addingScore = true;
            StartCoroutine(AddingScore());
        }
        // else if (isEnded)
        // {
        //     this.gameObject.GetComponent<GameOver>().enabled = true;
        // }
    }

    IEnumerator AddingScore()
    {
        if (isBoosted)
            scoreCount += 2;
        else
            scoreCount += 1;

        scoreCountDisplay.text = scoreCount.ToString() + "" + goalDisplay;
        scoreEndDisplay.GetComponent<Text>().text = scoreCount + "";
        scoreStageFinished.GetComponent<Text>().text = scoreCount + "";

        yield return new WaitForSeconds(scoreDelay);
        addingScore = false;
    }
}
