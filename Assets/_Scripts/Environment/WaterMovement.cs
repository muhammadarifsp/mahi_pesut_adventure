using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public GameObject playerObj;

    void Update()
    {
        // Debug.Log(transform.position);

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            playerObj.transform.position.z + 70
        );
    }
}
