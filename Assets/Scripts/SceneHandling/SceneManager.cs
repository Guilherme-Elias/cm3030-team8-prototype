using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class SceneManager : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Loading Faction Chooser scene...");
        SceneManager.LoadScene("FactionChooser");
    }

    public void OpenHowToPlay()
    {
        Debug.Log("Loading HowToPlay UI...");
        SceneManager.LoadScene("HowToPlay");
    }

    public void BackToMainMenu()
    {
        Debug.Log("Returning to Welcome Screen...");
        SceneManager.LoadScene("WelcomeScreen");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

        // If running in the Unity Editor, stop playing
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
