using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] public float lifeSpame = 5f;


    // Update is called once per frame
    void Update()
    {

        if (lifeSpame <= 0)
        {
            Destroy(gameObject);
        }
        else { lifeSpame -= Time.deltaTime; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        { Destroy(gameObject); }

    }
}
