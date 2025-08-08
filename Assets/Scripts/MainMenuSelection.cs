using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSelection : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void ExitGame()
    {
        Debug.Log("Seeya space cowboy...");
        Application.Quit(); // test
    }
}
