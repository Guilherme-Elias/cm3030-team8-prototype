using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public event Action shoot;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shoot?.Invoke();
        }
    }
}
