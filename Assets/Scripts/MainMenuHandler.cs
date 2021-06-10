using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{

    public Animator mainMenuAnimator;
    public AudioClip buttonPressAudioClip;

    public void OnClickStart()
    {
        mainMenuAnimator.SetTrigger("ClickStart");
        //LevelLoader.GetInstance().Load(LevelLoader.Scene.GameScene);

        PlaysoundOnButtonPressUI();
    }

    public void OnClickChapter1()
    {
        mainMenuAnimator.SetTrigger("ClickChapter1");

        PlaysoundOnButtonPressUI();
    }

    public void OnClickChapter1Start()
    {
        PlayerPrefs.SetInt("shouldCutsceneBePlayed", 1);
        LevelLoader.GetInstance().Load(LevelLoader.Scene.TheResistance);

        PlaysoundOnButtonPressUI();
    }

    private void PlaysoundOnButtonPressUI()
    {
        AudioSource.PlayClipAtPoint(buttonPressAudioClip, new Vector3(0, 0, -10), 1.0f);
    }
}
