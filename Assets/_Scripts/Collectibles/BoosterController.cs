using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoosterController : MonoBehaviour
{
    public GameObject[] boosterDisplay;
    public TMP_Text[] boosterText;
    public static bool boosterImmune,
        boosterScore,
        boosterCoin;

    private void Update()
    {
        if (boosterImmune || boosterCoin || boosterScore)
        {
            StartCoroutine(DisplayTimer());
        }
    }

    // public static void setBoosterImmune()
    // {
    //     boosterImmune = true;
    // }

    IEnumerator DisplayTimer()
    {
        if (boosterImmune)
        {
            boosterImmune = false; // set back to false
            int boosterTimeout = (int)PlayerController.boosterDelay; // get timeout booster

            boosterDisplay[boosterDisplay.Length - 1].SetActive(true);
            boosterDisplay[0].SetActive(true);

            for (int i = boosterTimeout; i > 0; i--)
            {
                boosterText[0].SetText(i.ToString());
                yield return new WaitForSeconds(1f);
            }
            boosterDisplay[0].SetActive(false);
        }

        boosterDisplay[boosterDisplay.Length - 1].SetActive(false);
        yield return new WaitForSeconds(.5f);
    }
}
