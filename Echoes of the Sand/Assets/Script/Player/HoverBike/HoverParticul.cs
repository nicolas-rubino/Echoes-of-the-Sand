using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(HoverBike))]
public class HoverParticul : MonoBehaviour
{
    HoverBike bike;
    [SerializeField] GameObject hoverEffect;
    [SerializeField] List<GameObject> hoverParticles;

    [SerializeField] GameObject engine;

    TrailRenderer engineTrail;


    [SerializeField] GameObject dustTrail;
    [SerializeField] VisualEffect VFXdustTrail;


    private void Awake()
    {
        bike = GetComponent<HoverBike>();

        for (int i = 0; i < bike.listeRayEngine.Count; i++)
        {
            GameObject objecInstance = Instantiate(hoverEffect, bike.listeRayEngine[i].transform.position, Quaternion.identity);
            objecInstance.transform.parent = this.transform;
            hoverParticles.Add(objecInstance);
        }


        engineTrail = GetComponentInChildren<TrailRenderer>();
        
        //VFXdustTrail.SendEvent("OnPlay");
        VFXdustTrail.enabled = false;
        VFXdustTrail.Play();
        VFXdustTrail.playRate = 1f;

        Debug.Log(VFXdustTrail.name);
    }

    private void Start()
    {
           VFXdustTrail.enabled = true;
    }

    void Update()
    {
        HoverEffect();

        EngineEffect();

        DustTrail();
    }

    private void EngineEffect()
    { 

        if (bike.playerMount)
        {
            engineTrail.emitting = true;
        }
        else
        {
           engineTrail.emitting = false;
        }
    }

    private void HoverEffect()
    {
        for (int i = 0; i < hoverParticles.Count; i++)
        {
            ParticleSystem particul = hoverParticles[i].GetComponent<ParticleSystem>();

            // Lance un rayon vers le bas depuis la position de l'objet
            Ray ray = new Ray(bike.listeRayEngine[i].transform.position, Vector3.down);
            RaycastHit hit;

            if (bike.playerMount)
            {
                // Vérifie si le rayon touche quelque chose
                if (Physics.Raycast(ray, out hit, bike.maxHover+1f, bike.layerMask))
                {
                    hoverParticles[i].transform.position = hit.point;
                    hoverParticles[i].transform.rotation = Quaternion.LookRotation(hit.normal);
                    particul.Play();
                  
                    Debug.DrawLine(ray.origin, hit.point, Color.magenta);
                }
                else
                {
                    particul.Stop();
                }

            }
            else
            {
                particul.Stop();
            }

        }

    }

    void DustTrail()
    {
        Ray ray = new Ray(engine.transform.position, Vector3.down);
        RaycastHit hit;

        if (bike.playerMount)
        {

            if (Physics.Raycast(ray, out hit, bike.maxHover+5f, bike.layerMask))
            {
                dustTrail.transform.position = hit.point;
                VFXdustTrail.Play();
                Debug.DrawLine(ray.origin, hit.point, Color.magenta);
            }
            else
            {
                VFXdustTrail.Stop();
            }

        }
        else
        {
            VFXdustTrail.Stop();
        }
    }
}
