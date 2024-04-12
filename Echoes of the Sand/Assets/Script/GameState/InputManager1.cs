using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager1 : MonoBehaviour
{
    PlayerControls controls;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Interactor interactor;
    [SerializeField] HoverBike hoverBike;
    [SerializeField] CameraController cameraController;
    [SerializeField] Aim aim;
    [SerializeField] Gun gun;

    public static bool isAimingInput = false;
    public static bool isShooting = false;

    private void Awake()
    {
        //controls = new PlayerControls();

        //desactif l'affichage de la sourie et loock sa position    
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        //controls.PlayerMovement.Shooting.performed += context => isShooting = true;
        //controls.PlayerMovement.Shooting.canceled += context => isShooting = false;

        //controls.PlayerMovement.Aiming.performed += context => isAimingInput = true;
        //controls.PlayerMovement.Aiming.canceled += context => isAimingInput = false;
        //controls.Enable();
       
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (hoverBike.playerMount)
        {
            hoverBike.OnMove(context);
        }
        else
        {
            playerMovement.OnMove(context);
        }

    }

    public void OnSprint(InputAction.CallbackContext context)
    {
       
        if (hoverBike.playerMount)
        {
           hoverBike.OnBoost(context);
            
        }
        else
        {
            playerMovement.OnSprint(context);
        }
    }

    public void OnChangeCam(InputAction.CallbackContext context)
    {
       
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interactor.OnInteract(context);

    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aim.OnAim(context);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        gun.OnShoot(context);
    }
























    //PlayerControls playerControls;
    //PlayerMovement playerMovement;
    //AnimatorManager animatorManager;

    //public Vector2 movementInput;

    //public float moveAmount;
    //public float verticalInput;
    //public float horizontalInput;

    //public bool b_Input_Temp;

    //private void Awake()
    //{
    //    animatorManager = GetComponent<AnimatorManager>();
    //    playerMovement = GetComponent<PlayerMovement>();
    //}

    //private void OnEnable()
    //{
    //    if (playerControls == null)
    //    {
    //        playerControls = new PlayerControls();

    //        playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

    //        //playerControls.PlayerActions.B_Sprint_Temp.performed += i => b_Input_Temp = true;        //when L3 is performed, the bool becomes true
    //        //playerControls.PlayerActions.B_Sprint_Temp.canceled += i => b_Input_Temp = false;        //when the button is released, it becomes false
    //    }

    //    playerControls.Enable();
    //}

    //private void OnDisable()
    //{
    //    playerControls.Disable();
    //}

    //public void HandleAllInputs()
    //{
    //    HandleMovementInput();
    //    //HandleSprintingInput();
    //}

    //private void HandleMovementInput()
    //{
    //    verticalInput = movementInput.y;
    //    horizontalInput = movementInput.x;
    //    moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    //    //animatorManager.UpdateAnimatorValues(0, moveAmount, playerMovement.isSprinting);
    //}

    //private void HandleSprintingInput()
    //{
    //    if (b_Input_Temp && moveAmount > 0.5f)
    //    {
    //        playerMovement.isSprinting = true;
    //    }
    //    else
    //    {
    //        playerMovement.isSprinting = false;
    //    }
    //}
}
