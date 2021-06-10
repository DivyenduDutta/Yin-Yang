using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    public AudioClip mainMenu1;
    public AudioClip mainMenu2;

    private AudioSource mainMenuAudioSource;

    private void Awake()
    {
        mainMenuAudioSource = GetComponent<AudioSource>();

        if (Random.Range(1, 100) < 50)
        {
            mainMenuAudioSource.clip = mainMenu1;
        }
        else
        {
            mainMenuAudioSource.clip = mainMenu2;
        }

        mainMenuAudioSource.Play();
    }
}
