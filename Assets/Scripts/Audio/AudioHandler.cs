using UnityEngine;

public static class AudioHandler
{
    public enum Sound
    {
        ButtonPressUI,
        EnemyDestroy,
        EnemyDeath,
        SwitchPolarity,
        PlayerDamage,
        MainMenu,
        MainMenuOriental
    }

    public static void PlayAudio(Sound sound, float volume)
    {
        AudioSource.PlayClipAtPoint(GetAudioClip(sound), new Vector3(0f, 0f, -10.0f), volume);
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundClip in GameAssets.GetInstance().soundClipArray)
        {
            if (soundClip.sound == sound)
            {
                return soundClip.audioClip;
            }
        }

        Debug.LogErrorFormat("Sound {0} not found. Please check!!", sound.ToString());
        return null;
    }
}
