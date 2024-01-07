using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableController : MonoBehaviour
{
    public static int coinCount;
    public float rotateCoinSpeeds;
    public static float rotateCoinSpeed;
    public GameObject coinCountDisplay;
    public GameObject coinEndDisplay;
    public GameObject coinStageFinished;

    private void Awake()
    {
        coinCount = 0;
    }

    private void Start()
    {
        rotateCoinSpeed = rotateCoinSpeeds;
    }

    void Update()
    {
        coinCountDisplay.GetComponent<Text>().text = coinCount + ""; // display saat main
        coinEndDisplay.GetComponent<Text>().text = coinCount + ""; // display saat gameover
        coinStageFinished.GetComponent<Text>().text = coinCount + ""; // display saat stage finished
    }
}
