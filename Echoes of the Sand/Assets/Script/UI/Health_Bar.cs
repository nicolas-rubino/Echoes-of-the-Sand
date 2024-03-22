//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    [SerializeField] Slider healthBarValue;
    [SerializeField] UnityEngine.UI.Image fill_color;

    [SerializeField] float r, g, b, a;
    [SerializeField] bool isFuel;

    void Start()
    {
        r = fill_color.color.r;
        g = fill_color.color.g;
        b = fill_color.color.b;
        a = fill_color.color.a;
    }

    // Update is called once per frame
    public void Hit()
    {

        healthBarValue.value -= 0.05f;
        if (!isFuel)
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
}
