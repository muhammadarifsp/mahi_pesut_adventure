using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinFx;
    public static bool isBoosted;

    private void Awake()
    {
        isBoosted = false;
    }

    void OnTriggerEnter(Collider other)
    {
        coinFx.Play();
        if (isBoosted)
        {
            CollectableController.coinCount += 2;
        }
        else
        {
            CollectableController.coinCount += 1;
        }
        this.gameObject.SetActive(false);
    }
}
