using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.6f;

    public static LevelLoader instance;

    public static LevelLoader GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }

    //level names match the scene names. Make sure to add a new entry made here to GameConstants/scenes enum
    public enum Scene
    {
        MainMenu,
        TheResistance
        
    }
    public void Load(Scene scene)
    {
        StartCoroutine(LoadSynchronously(scene));
    }

    IEnumerator LoadSynchronously(Scene scene)
    {
        transition.SetTrigger("ScreenTransit");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene.ToString());
    }
}
