using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxAmmo(int maxAmmo)
    {
        slider.maxValue = maxAmmo;
        slider.value = maxAmmo;
    }

    public void SetAmmo(int ammo)
    {
        slider.value = ammo;
    }
}
