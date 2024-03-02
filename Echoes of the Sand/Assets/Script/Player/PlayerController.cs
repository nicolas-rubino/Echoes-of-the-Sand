using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField] float movementSpeed = 1.0f;
    [SerializeField] Animator animator;


    private bool isMoving = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        Move();

    }
    private void UpdateAnimation()
    {
        animator.SetFloat("xSpeed", InputManager.movementInput.x);
        animator.SetFloat("zSpeed", InputManager.movementInput.y);
    }

    private void Move()
    {
        Vector3 moveInput = new Vector3(InputManager.movementInput.x, 0, InputManager.movementInput.y);
        Vector3 moveDirection = (transform.forward * moveInput.z + transform.right * moveInput.x).normalized;
        moveDirection.y = 0;

        if(moveInput.z > 0)
        {
            if(!isMoving)
            {
                moveDirection = Camera.main.transform.forward;
                moveDirection.y = 0;

                TurnTowardsMovementDirection(moveDirection);
                isMoving = true;
            }
        }
        else
        {
            isMoving = false;
        }

        Vector3 movement = moveDirection.normalized * (movementSpeed * Time.deltaTime);

        rb.MovePosition(rb.position + movement);

    }

    private void TurnTowardsMovementDirection(Vector3 moveDirection)
    {
        // Calculate the target rotation based on the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        rb.rotation = targetRotation;
    }
}
