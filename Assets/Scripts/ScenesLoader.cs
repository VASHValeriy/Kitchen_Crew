using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesLoader {

    private static string _stringGameSceneLoader;

    private const string LOADING_SCENE = "LoadingScene";

    public static void LoadScene(string sceneName) {
        _stringGameSceneLoader = sceneName;

        SceneManager.LoadScene(LOADING_SCENE);
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(_stringGameSceneLoader);
    }

}