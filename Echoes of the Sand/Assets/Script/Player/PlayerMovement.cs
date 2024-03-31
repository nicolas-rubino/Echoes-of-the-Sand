using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Windows;
using Unity.Mathematics;


[RequireComponent(typeof(Aim))]
public class PlayerMovement : MonoBehaviour
{   
    //Reference variables
    Rigidbody rb;
    Aim aim;
    bool isAiming = false;

    //[SerializeField] private CinemachineFreeLook cam;
    [SerializeField] private Camera cam;

    //Variables to store player input values
    Vector3 currentMovement;
    Quaternion TargetRotation;
    bool isMovementPressed;


    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;               //gives a little boost off the ledge when falling
    public float fallingVelocity;
    public float rayCastHeightOffSet = 0.5f;    //this way the raycast won't start below the ground
    public float distanceToGround = 0f;
    public LayerMask groundLayer;               //invisible check for when the player's feet touch the ground

    [Header("Movement Flags")]
    public bool isIdle = true;
    public bool isWalking = false;
    public bool isRunning = false;
    public bool isSprinting = false;

    public bool isGrounded = true;
    public bool isFalling = false;

    [Header("Movement Speed")]
    [SerializeField] private float moveSpeedSwitchValue = 0.5f;
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5;
    public float sprintingSpeed = 7;
    public float rotationSpeed = 10;

    public float CurrentMoveSpeed
    {
        get
        {
            if (isWalking)
            {
                return walkingSpeed;
            }

            if (isRunning)
            {
                return runningSpeed;
            }

            if (isSprinting)
            {
                return sprintingSpeed;
            }

            else
            {
                return 0;
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animatorManager = GetComponent<AnimatorManager>();
        aim = GetComponent<Aim>();
    }

    private void FixedUpdate()
    {
        isAiming = aim.isAming;

        ChageRotation();
        OnGround();

        Vector3 move = new Vector3(currentMovement.x * CurrentMoveSpeed, rb.velocity.y, currentMovement.z * CurrentMoveSpeed);
        move = Quaternion.AngleAxis(/*cam.m_XAxis.Value*/cam.transform.rotation.eulerAngles.y, Vector3.up) * move;
        //Debug.Log(move);    
        //Debug.Log(cam.transform.rotation.eulerAngles.y);
        rb.velocity = move;

    }

    public void OnMove(InputAction.CallbackContext context)
    {

        Vector2 currentInput = context.ReadValue<Vector2>();
        currentMovement = new Vector3(currentInput.x, 0, currentInput.y);

        float moveAmount;
        float verticalInput;
        float horizontalInput;

        verticalInput = currentInput.y;
        horizontalInput = currentInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        if (moveAmount <= moveSpeedSwitchValue && moveAmount > 0)
        {
            isIdle = false;
            isWalking = true;
            isRunning = false;
            isSprinting = false;
        }
        else if (moveAmount > moveSpeedSwitchValue && !isSprinting)
        {
            isIdle = false;
            isWalking = false;
            isRunning = true;
            isSprinting = false;
        }
        else
        {
            isIdle = true;
            isWalking = false;
            isRunning = false;
            isSprinting = false;
        }


    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started /*&& isRunning*/)
        {
            isIdle = false;
            isWalking = false;
            isRunning = false;
            isSprinting = true;
        }
    }

    public void ChageRotation()
    {
        Vector2 input = new Vector2(currentMovement.x, currentMovement.z);

        if (!isIdle || isAiming)
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + /*cam.m_XAxis.Value*/ cam.transform.rotation.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnGround() 
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit,math.INFINITY, groundLayer))
        {
            distanceToGround = hit.distance;
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            if(distanceToGround >= 0.1f)
            {
                isGrounded = false;
                isFalling = true;
            }
            else
            {
                isGrounded = true;
                isFalling = false;
            }

        }

    }

    //private void Awake()
    //{

    //    playerManager = GetComponent<PlayerManager>();      //it is resting on the same game object as the script (as all other GetComponent lines)
    //    animatorManager = GetComponent<AnimatorManager>();
    //    inputManager = GetComponent<InputManager>();
    //    characterController = GetComponent<CharacterController>();
    //}

    ////Activating the action map
    //private void OnEnable()
    //{
    //    playerControls.PlayerMovement.Enable();
    //}

    //private void OnDisable()
    //{
    //    playerControls.PlayerMovement.Disable();
    //}


    #region Other

    //public void HandleAllMovement()
    //{
    //    //HandleFallingAndLanding();      //called first, because whatever action is being done, if we start falling, the character must start falling right away

    //    if (playerManager.isInteracting)
    //        return;     //returning true so that no locomotion actions can be executed while interacting (falling, interacting with prompts, opening a door, while in dialogue, etc.)

    //    HandleMovement();
    //    HandleRotation();
    //}

    //private void HandleMovement()
    //{
    //    //moveDirection = cameraObject.forward * inputManager.verticalInput;
    //    //moveDirection += cameraObject.right * inputManager.horizontalInput;
    //    //moveDirection.Normalize();
    //    //moveDirection.y = 0;

    //    if (isSprinting)
    //    {
    //        moveDirection = moveDirection * sprintingSpeed;
    //    }
    //    else
    //    {
    //        if (inputManager.moveAmount >= 0.5f)
    //        {
    //            moveDirection = moveDirection * runningSpeed;
    //        }
    //        else
    //        {
    //            moveDirection = moveDirection * walkingSpeed;
    //        }
    //    }

    //    Vector3 movementVelocity = moveDirection;
    //    //characterController.velocity = movementVelocity;
    //}

    //private void HandleRotation()
    //{
    //    Vector3 targetDirection = Vector3.zero;

    //    //targetDirection = cameraObject.forward * inputManager.verticalInput;
    //    //targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
    //    //targetDirection.Normalize();
    //    //targetDirection.y = 0;

    //    if (targetDirection == Vector3.zero)
    //    {
    //        targetDirection = transform.forward;
    //    }

    //    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
    //    Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    //    transform.rotation = playerRotation;
    //}

    //private void OnFall()
    //{
    //    RaycastHit hit;
    //    Vector3 rayCastOrigin = transform.position;
    //    rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

    //    if (!isGrounded)
    //    {
    //        if (!playerManager.isInteracting)
    //        {
    //            animatorManager.PlayTargetAnimation("Falling", true);
    //        }

    //        //the longer we are in the air, the quicker we are going to fall
    //        inAirTimer = inAirTimer + Time.deltaTime;
    //        playerRigidbody.AddForce(transform.forward * leapingVelocity);
    //        playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
    //    }

    //    if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
    //    {
    //        if (!isGrounded && !playerManager.isInteracting)
    //        {
    //            animatorManager.PlayTargetAnimation("Landing", true);
    //        }

    //        inAirTimer = 0;
    //        isGrounded = true;
    //    }
    //    else
    //    {
    //        isGrounded = false;
    //    }
    //}

    #endregion


    //enlever le curseur lorsqu'en mode jeu (après clic dans l'écran)
    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //}
}
