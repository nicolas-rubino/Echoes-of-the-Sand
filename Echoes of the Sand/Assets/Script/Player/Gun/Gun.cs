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

    [SerializeField] float bulletSpeed = 10;
    [SerializeField] float shootingRate = 2f;
    [SerializeField] float shootingCooldown = 0f;

    //Energy Bar
    [SerializeField] GameObject energyBar;
    [SerializeField] float energyUsedPerShot = 0.5f;

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

            if (energyBar.GetComponent<Health_Bar>().isEmpty(energyUsedPerShot))
            {
                return;
            }
            /*
            Vector3 cameraForward = Camera.main.transform.forward;
            
            Vector3 screenCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));


            // Calculate bullet spawn point direction to center of the screen 
            Vector3 directionToCenter = (screenCenter - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            //Set velocity of bullet towards center
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bullet != null)
            {
                //bullet.transform.Translate(directionToCenter * bulletSpeed * Time.deltaTime);
                
                bulletRigidbody.velocity = directionToCenter * bulletSpeed;
            }
            */
            // Get the center of the screen in world space
            Vector3 screenCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));

            // Calculate direction towards the center of the screen from the player's position
            Vector3 direction = (screenCenter - bulletSpawnPoint.transform.position).normalized;

            // Raycast from the camera through the center of the screen
            Ray ray = new Ray(Camera.main.transform.position, -direction);

            // Check if the ray hits something in the scene
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Calculate direction from player to the point where the ray hit
                direction = (hit.point - bulletSpawnPoint.transform.position).normalized;
            }

            // Instantiate the bullet at the current position of the player
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);

            // Access the Rigidbody component of the instantiated bullet and set its velocity
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = -direction * bulletSpeed;
            }

            energyBar.GetComponent<Health_Bar>().useEnergy(energyUsedPerShot);
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

