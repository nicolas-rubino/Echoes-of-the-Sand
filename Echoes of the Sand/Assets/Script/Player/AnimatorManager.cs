using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Aim))]
public class AnimatorManager : MonoBehaviour
{

    Animator animator;
    //InputManager inputManager;
    PlayerMovement playerMovement;
    Aim aim;
    int horizontal;
    int vertical;
    bool isIdle = true;
    bool isWalking = false;
    bool isRunning = false;
    bool isSprinting = false;
    bool isFalling = false;
    bool isAiming = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        aim = GetComponent<Aim>();

        horizontal = Animator.StringToHash("Horizontal");       //reference to the parameters in the Animator
        vertical = Animator.StringToHash("Vertical");
        
    }

    private void Update()
    {
        isIdle = playerMovement.isIdle;
        isWalking = playerMovement.isWalking;
        isRunning = playerMovement.isRunning;
        isSprinting = playerMovement.isSprinting;
        isFalling = playerMovement.isFalling;
        isAiming = aim.isAming;

        animator.SetBool(AnimationString.isIdle, isIdle);
        animator.SetBool(AnimationString.isWalking, isWalking);
        animator.SetBool(AnimationString.isRunning, isRunning);
        animator.SetBool(AnimationString.isSprinting, isSprinting);
        animator.SetBool(AnimationString.isFalling, isFalling);
        animator.SetBool(AnimationString.isAiming, isAiming);
    }

    //will play any animation that we want when called, isInteracting is the bool the checks if the player is locked in an animation
    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        //Animation Snapping
        float snappedHorizontal;
        float snappedVertical;

        #region Snapped Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion

        #region Snapped Vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        if (isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
