using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject displayPanel,
        gameOverPanel,
        fadeOut;

    void Start()
    {
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(3);
        
        displayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
