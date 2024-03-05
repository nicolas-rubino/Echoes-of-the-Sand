using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    private float detectionRange = 15;
    private IInteractable interactable;
    bool findBike = false;

    Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0f);
    [SerializeField] LayerMask layerMask;


    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    //interact (faster way to do the null check)
        //    interactable?.Interact();
        //}
        if (!findBike)
        {
            RaycastDetection();
        }

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactable?.Interact();
        }

    }

    private void RaycastDetection()     //le ray va pas a la bonne place
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



        // Lancer un rayon depuis le centre de l'écran
        Ray ray = Camera.main.ViewportPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, detectionRange, layerMask))
        {
            IInteractable i = hit.collider.GetComponent<IInteractable>();
            if (i != null)
            {
                interactable = i;
            }
            else
            {
                interactable = null;
            }
            //Debug.Log("Objet touché : " + hit.transform.name + ", Tag : " + hit.transform.tag);
        }
        else
        {
            interactable = null;
        }

        //Ray ray = new Ray(Camera.main.transform.position + transform.up, Camera.main.transform.forward * detectionRange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = Camera.main.ViewportPointToRay(screenCenter);
        Gizmos.DrawRay(ray.origin, ray.direction * detectionRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bike"))
        {
            //check if the object is interactable
            IInteractable i = other.GetComponent<IInteractable>();
            if (i != null)
            {
                interactable = i;
                findBike = true;
            }
            Debug.Log("Devrait marcher");
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bike"))
        {
            //remove the interaction option
            IInteractable i = other.GetComponent<IInteractable>();
            if (i == interactable)
            {
                interactable = null;
                findBike = false;
            }
        }

    }
}
