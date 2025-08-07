using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("A new game will start!");
    }

    public void OpenSettings()
    {
        Debug.Log("Settings pannel will be opened.");
    }

    public void ExitGame()
    {
        Debug.Log("Seeya!");
    }
}
