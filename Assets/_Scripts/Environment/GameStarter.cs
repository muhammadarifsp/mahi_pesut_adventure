using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject countdown3,
        countdown2,
        countdown1,
        countdownGo,
        fadeIn;

    public AudioSource readyFX,
        goFX;

    void Start()
    {
        StartCoroutine(CountSequence());
    }

    IEnumerator CountSequence()
    {
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        countdown3.SetActive(true);
        readyFX.Play();
        yield return new WaitForSeconds(1f);
        countdown2.SetActive(true);
        readyFX.Play();
        yield return new WaitForSeconds(1f);
        countdown1.SetActive(true);
        readyFX.Play();
        yield return new WaitForSeconds(1f);
        countdownGo.SetActive(true);
        goFX.Play();

        PlayerController.isStarted = true;
    }
}
