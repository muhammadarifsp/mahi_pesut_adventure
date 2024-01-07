using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.transform.tag);
        // if (other.transform.tag == "Obstacle")
        // {
        //     other.gameObject.SetActive(false);
        // }
    }
}
