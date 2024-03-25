using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(HoverBike))]
public class HoverParticul : MonoBehaviour
{
    HoverBike bike;
    [SerializeField] GameObject hoverEffect;
    [SerializeField] List<GameObject> hoverParticles;


    private void Awake()
    {
        bike = GetComponent<HoverBike>();

        for (int i = 0; i < bike.listeRayEngine.Count; i++)
        {
            GameObject objecInstance = Instantiate(hoverEffect, bike.listeRayEngine[i].transform.position, Quaternion.identity);
            objecInstance.transform.parent = this.transform;
            hoverParticles.Add(objecInstance);
        }
    }

    void Update()
    {

        for (int i = 0; i < hoverParticles.Count; i++)
        {
            ParticleSystem particul = hoverParticles[i].GetComponent<ParticleSystem>();

            // Lance un rayon vers le bas depuis la position de l'objet
            Ray ray = new Ray(hoverParticles[i].transform.position, Vector3.down);
            RaycastHit hit;

            if (bike.playerMount)
            {
                // Vérifie si le rayon touche quelque chose
                if (Physics.Raycast(ray, out hit, bike.maxHover+1f, bike.layerMask))
                {
                    hoverParticles[i].transform.position = hit.point;
                    hoverParticles[i].transform.rotation = Quaternion.LookRotation(hit.normal);
                    particul.Play();

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
}
