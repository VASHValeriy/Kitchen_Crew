using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesLoader {

    private static string _stringGameSceneLoader;

    private const string LOADING_SCENE = "LoadingScene";

    public enum Scenes {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }

    private static Scenes _sceneToLoad;

    public static void LoadScene(Scenes scene) {
        _sceneToLoad = scene;

        SceneManager.LoadScene(LOADING_SCENE);
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(_sceneToLoad.ToString());
    }

}