using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private CharacterController controller;

    //
    private Vector3 direction;

    // forward speed
    public float fwSpeed;

    // point that interprise path
    private int pointPath = 0; // -1:left, 0:center, 1:right

    // distance between path
    public float pathDistance = 4;

    //
    public float jumpForce;

    //
    public float gravity = -20;

    // player animator
    public Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isGameStarted", true);
        direction.z = fwSpeed;

        //input events

        if (controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.fixedDeltaTime;
        }

        if (Input.GetKeyDown(KeyCode.D) && pointPath < 1)
        {
            pointPath++;
        }
        else if (Input.GetKeyDown(KeyCode.A) && pointPath > -1)
        {
            pointPath--;
        }

        //update movement
        Vector3 targetPosition =
            transform.position.z * transform.forward + transform.position.y * transform.up;

        if (pointPath == -1)
        {
            targetPosition += Vector3.left * pathDistance;
        }
        else if (pointPath == 1)
        {
            targetPosition += Vector3.right * pathDistance;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            80 * Time.fixedDeltaTime
        );
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}
