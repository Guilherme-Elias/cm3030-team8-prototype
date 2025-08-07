using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("A new game will start!");
        // testing actually starting the game
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenSettings()
    {
        Debug.Log("Settings pannel will be opened.");
    }

    public void ExitGame()
    {
        Debug.Log("Seeya!");
        Application.Quit(); // test
    }
}
