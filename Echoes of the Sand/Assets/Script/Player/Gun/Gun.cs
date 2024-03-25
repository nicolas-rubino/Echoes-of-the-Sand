using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;

    [SerializeField] Transform holster;
    [SerializeField] Transform aimPos;

    [SerializeField] float bulletSpeed = 1000f;
    [SerializeField] float shootingRate = 2f;
    [SerializeField] float shootingCooldown = 0f;

    Aim aim;

    public bool isAiming, isShooting /*shotOnce*/ ;

    private void Awake()
    {
        aim = GetComponentInParent<Aim>();
    }


    // Update is called once per frame
    void Update()
    {
        //isAiming = InputManager.isAimingInput;
        //isShooting = InputManager.isShooting;
        //shotOnce = false;

        isAiming = aim.isAming;

        if (shootingCooldown > 0)
        {
            shootingCooldown -= Time.deltaTime;
        }

        if (isAiming)
        {
            if (isShooting && shootingCooldown <= 0)
            {
                //Shoot();
                shootingCooldown = shootingRate;
            }

            transform.position = aimPos.position;
            transform.rotation = aimPos.rotation;
        }
        else
        {
            transform.position = holster.position;
            transform.rotation = holster.rotation;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("SHOT");

            // Calculate bullet spawn point direction to center of the screen 
            Vector3 directionToCenter = (getMiddleOfScreen() - bulletSpawnPoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(directionToCenter));

            //Set velocity of bullet towards center
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bullet != null)
            {
                bulletRigidbody.velocity = transform.forward * bulletSpeed;
            }
        }

        // shotOnce = true;

    }

    private Vector3 getMiddleOfScreen()
    {
        // Calculate the direction towards the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Vector3 centerScreenDirection = ray.direction;
        return centerScreenDirection;
    }

}

