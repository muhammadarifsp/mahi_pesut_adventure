using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectBooster : MonoBehaviour
{
    private List<string> boosterName = new List<string> { "Score", "Coin", "Immune" };

    private void Awake()
    {
        RandomSetActive();
    }

    private void RandomSetActive()
    {
        // posisi default di tengah
        // chance 50% posisi ke kiri atau kanan
        if (Random.value > .5f)
        {
            // chance 50% posisi ke kanan
            if (Random.value > .5f)
                this.transform.position = new Vector3(
                    3,
                    transform.position.y,
                    transform.position.z
                );
            // chance 50% posisi ke kiri
            else
                this.transform.position = new Vector3(
                    -3,
                    transform.position.y,
                    transform.position.z
                );
        }
        if (Random.value > .7f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
