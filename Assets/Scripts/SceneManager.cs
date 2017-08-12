using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EScenes {
    Logo,
    MainMenu,
    Game,
}

public class SceneManager : MonoBehaviour {

    public delegate void NewSceneLoaded(EScenes newScene);
    public event NewSceneLoaded OnNewSceneLoaded;

    public EScenes _currentScene;

    public void LoadNewScene(EScenes scene) {
        _currentScene = scene;

        int sceneIndex = ESceneToIndex(scene);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);

        if (OnNewSceneLoaded != null) OnNewSceneLoaded(scene);
    } // LoadNewScene

    private int ESceneToIndex(EScenes scene) {
        switch (scene) {
            case EScenes.Logo: return 0;
            case EScenes.MainMenu: return 1;
            case EScenes.Game: return 2;
        }
        return 0;
    } // ESceneToIndex

    public bool IsMainMenu() {
        return _currentScene == EScenes.MainMenu;
    } // IsMainMenu

    public bool IsGame() {
        return _currentScene == EScenes.Game;
    } // IsGame
}
