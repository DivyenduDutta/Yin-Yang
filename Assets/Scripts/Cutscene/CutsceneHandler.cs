using UnityEngine;
using UnityEngine.Playables;

public class CutsceneHandler : MonoBehaviour
{
    public GameObject koel;
    public UnityEngine.Video.VideoPlayer cutscenePlayer;
    public PlayableDirector levelOpNameCutscene;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("shouldCutsceneBePlayed") != 1)
        {
            CutsceneEnd();
        }
        cutscenePlayer.loopPointReached += EndReached;
    }

    private void OnDestroy()
    {
        cutscenePlayer.loopPointReached += EndReached;
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        CutsceneEnd();
    }

    public void CutsceneEnd()
    {
        MainGameAudio.GetInstance().PlayPartOneAudio();
        koel.SetActive(true);
        this.gameObject.SetActive(false);
        levelOpNameCutscene.Play();
    }
}
