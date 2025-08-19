using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxAmmo(float maxAmmo)
    {
        slider.maxValue = maxAmmo;
        slider.value = maxAmmo;
    }

    public void SetAmmo(float ammo)
    {
        slider.value = ammo;
    }
}
