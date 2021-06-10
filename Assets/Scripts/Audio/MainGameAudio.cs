using UnityEngine;

public class MainGameAudio : MonoBehaviour
{
    public GameObject partOneAudio;
    public GameObject bossAudio;

    public static MainGameAudio instance;

    public static MainGameAudio GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public void PlayPartOneAudio()
    {
        if (bossAudio.activeSelf == true)
        {
            bossAudio.SetActive(false);
        }

        partOneAudio.SetActive(true);
    }

    public void PlayBossAudio()
    {
        if (partOneAudio.activeSelf == true)
        {
            partOneAudio.SetActive(false);
        }

        bossAudio.SetActive(true);
    }

    public void StopGameAudio()
    {
        if (bossAudio.activeSelf == true)
        {
            bossAudio.SetActive(false);
        }

        if (partOneAudio.activeSelf == true)
        {
            partOneAudio.SetActive(false);
        }
    }

    public void FadePartOneAudio()
    {
        partOneAudio.GetComponent<Animator>().SetTrigger("PartOneFadeOut");
    }

    public void FadeBosseAudio()
    {
        bossAudio.GetComponent<Animator>().SetTrigger("BossAudioFadeOut");
    }

    public void FadeGameAudio()
    {
        if (bossAudio.activeSelf == true)
        {
            FadeBosseAudio();
        }

        if (partOneAudio.activeSelf == true)
        {
            FadePartOneAudio();
        }
    }
}
