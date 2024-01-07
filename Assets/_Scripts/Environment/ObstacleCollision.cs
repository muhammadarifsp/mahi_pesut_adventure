using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject charModel;
    public AudioSource deathFX;

    // void OnTriggerEnter(Collider other)
    // {
    //     this.gameObject.GetComponent<BoxCollider>().enabled = false;
    //     thePlayer.GetComponent<PlayerController>().enabled = false;

    //     charModel.GetComponent<Animator>().Play("Death");
    //     ScoreController.isEnded = true;
    //     deathFX.Play();
    // }
}
