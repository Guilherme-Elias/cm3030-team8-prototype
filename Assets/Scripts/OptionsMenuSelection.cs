using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuSelection : MonoBehaviour
{
    public void GoBack()
    {
        Debug.Log("Back to the main menu!");
        SceneManager.LoadScene("MainMenu");
    }
}
