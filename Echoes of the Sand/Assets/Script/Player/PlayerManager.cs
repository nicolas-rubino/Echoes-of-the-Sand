using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    Animator animator;
    InputManager inputManager;
    PlayerMovement playerMovement;

    [SerializeField] Health_Bar health; 

    public bool isInteracting;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        //playerMovement.HandleAllMovement();
    }

    private void LateUpdate()
    {

    }

    public void TakeDamage()
    {
        health.Hit();
    }
}
