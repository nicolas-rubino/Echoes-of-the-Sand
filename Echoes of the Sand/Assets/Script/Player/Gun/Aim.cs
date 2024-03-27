using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Aim : MonoBehaviour
{
    [SerializeField] HoverBike bike;

    [SerializeField] private Transform orignFocus;
    [SerializeField] private Transform aimFocus;
    [SerializeField] private CinemachineFreeLook cam;
    [SerializeField] private float aimDistance;
    [SerializeField] private float bikeDistance;
    public bool isAming = false;
    public bool isBike = false;

    [SerializeField] Vector3 ring;
    private Vector3 CurrentRing
    {
        get
        {

            if (isAming)
            {
                return new Vector3(topRing, midRing, bottomRing) / aimDistance;
            }
            if (isBike)
            {
                return new Vector3(topRing, midRing, bottomRing) * bikeDistance;
            }
            else
            {
                return new Vector3(topRing, midRing, bottomRing);
            }
        }

    }

    [SerializeField] private float topRing = 6;
    [SerializeField] private float midRing = 7;
    [SerializeField] private float bottomRing = 6;


    public void Awake()
    {
        //topRing = cam.m_Orbits[0].m_Radius;
        //midRing = cam.m_Orbits[1].m_Radius;
        //bottomRing = cam.m_Orbits[2].m_Radius;
    }

    public void Update()
    {
        isBike = bike.playerMount;
        ring = CurrentRing;
        SetRing();

    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cam.Follow = aimFocus;
            cam.LookAt = aimFocus;
            isAming = true;
            SetRing();
        }
        if (context.canceled)
        {
            cam.Follow = orignFocus;
            cam.LookAt = orignFocus;
            isAming = false;
            SetRing();
        }
    }

    public void SetRing()
    {
        if (isAming)
        {
            cam.m_Orbits[0].m_Radius = CurrentRing.x;
            cam.m_Orbits[1].m_Radius = CurrentRing.y;
            cam.m_Orbits[2].m_Radius = CurrentRing.z;
        }
        else
        {
            cam.m_Orbits[0].m_Radius = CurrentRing.x;
            cam.m_Orbits[1].m_Radius = CurrentRing.y;
            cam.m_Orbits[2].m_Radius = CurrentRing.z;
        }
    }

    internal void OnPlayerSeat()
    {
        if (isBike)
        {
            isBike = false;
        }
        else
        {
            isBike = true;
        }

    }

    //private void OnDisable()
    //{
    //    cam.m_Orbits[0].m_Radius = topRing;
    //    cam.m_Orbits[1].m_Radius = midRing;
    //    cam.m_Orbits[2].m_Radius = bottomRing;
    //}
}
