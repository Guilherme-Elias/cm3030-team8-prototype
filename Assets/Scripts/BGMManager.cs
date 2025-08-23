using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip bgmClip; // drag Bloodpath (Edit).wav in the Inspector
    private AudioSource audioSource;

    void Awake()
    {
        // ensure the music player won't be destroyed when switching scenes
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.7f; // adjust volume
        audioSource.Play();
    }
}
