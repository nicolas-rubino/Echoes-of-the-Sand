//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    [SerializeField] Slider healthBarValue;
    [SerializeField] UnityEngine.UI.Image fill_color;

    [SerializeField] float r, g, b, a;
    [SerializeField] bool isHealth;
    [SerializeField] bool isRechargable = false;
    [SerializeField] bool isFuel = false;

    void Start()
    {
        r = fill_color.color.r;
        g = fill_color.color.g;
        b = fill_color.color.b;
        a = fill_color.color.a;
    }

    //
    private void FixedUpdate()
    {
        if(isRechargable) {
            healthBarValue.value += 0.001f;
        }
        else if(isFuel)
        {
            healthBarValue.value += 0.0005f;
        }
    }

    // Update is called once per frame
    public void Hit()
    {

        healthBarValue.value -= 0.05f;
        if (isHealth)
        {

            // Changement de Vert a Jaune
            if (healthBarValue.value >= 0.5f)
            {
                fill_color.color = new Color((1 - healthBarValue.value) * 2, g, b, a);
            }
            else if (healthBarValue.value < 0.5f)
            {
                fill_color.color = new Color(1, (healthBarValue.value * 2), b, a);

            }
        }

    }

    public void useFuel(float amount)
    {
        if (healthBarValue.value > amount)
        {
            healthBarValue.value -= amount;
        }
        else
        {
            healthBarValue.value = 0;
        }
    }

    public void useEnergy(float amount)
    {
        if (healthBarValue.value > amount)
        {
            healthBarValue.value -= amount;
        }
        else
        {
            healthBarValue.value = 0;
        }
    }

    public bool isEmpty(float amountNeeded)
    {
        return healthBarValue.value < amountNeeded;
    }
    
    public bool isFull()
    {
        return healthBarValue.value == 1;
    }
}
