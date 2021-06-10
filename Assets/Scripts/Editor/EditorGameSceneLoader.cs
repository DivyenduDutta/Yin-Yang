using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorGameSceneLoader : Editor
{
    static string scenesFolderLocation = "Assets/Scenes/";
    static string sceneMainGame = "TheResistance.unity";
    static string sceneMainMenu = "MainMenu.unity";

    [MenuItem("Shmup/Scenes/Load Main menu")]
    public static void LoadMainMenuScene()
    {
        LoadScene(sceneMainMenu);
    }

    [MenuItem("Shmup/Scenes/Load The Resistance")]
    public static void LoadMainGameScene()
    {
        LoadScene(sceneMainGame);
    }

    static void LoadScene(string selectedScenePath)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenesFolderLocation + selectedScenePath, OpenSceneMode.Single);
        }
    }
}
