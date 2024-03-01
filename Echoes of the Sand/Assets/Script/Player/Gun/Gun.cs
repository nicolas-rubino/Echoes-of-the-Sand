using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;

    [SerializeField] float bulletSpeed = 1000f;
    [SerializeField] float shootingRate = 2f;
    [SerializeField] float shootingCooldown = 0f;

    bool isAiming, isShooting /*shotOnce*/ ;

    // Update is called once per frame
    void Update()
    {
        //isAiming = InputManager.isAimingInput;
        //isShooting = InputManager.isShooting;
        //shotOnce = false;

        if (shootingCooldown > 0)
        {
            shootingCooldown -= Time.deltaTime;
        }

        if (isAiming)
        {
            if (isShooting && shootingCooldown <= 0)
            {
                Shoot();
                shootingCooldown = shootingRate;
            }
        }
    }

    private void Shoot()
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

