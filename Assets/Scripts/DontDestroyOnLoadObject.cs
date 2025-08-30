using UnityEngine;

/// <summary>
/// Keep this GameObject alive when loading new scenes.
/// Attach this to the same object that has your AudioSource.
/// </summary>
public class DontDestroyOnLoadObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
