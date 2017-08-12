using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    private GameData _gameData;

    private SceneManager _sceneManager;
    private LevelManager _levelManager;



    private List<MonoBehaviourUpdate> _updates;

    public void Awake() {
        _gameData = GetComponent<GameData>();

        _sceneManager = GetComponent<SceneManager>();
        _sceneManager.OnNewSceneLoaded += OnNewSceneLoaded;


        _updates = new List<MonoBehaviourUpdate>();
    } // _sceneManager 

    public void Start() {
        Debug.Log("DebugForStartInGame");
        if (_sceneManager.IsGame())
            OnNewSceneLoaded(EScenes.Game);
    } // Start

    private void OnNewSceneLoaded(EScenes newScene) {
        if (newScene == EScenes.Game) {
            InitGame();
        }
    } // OnNewSceneLoaded

    private void InitGame() {
        _levelManager = FindObjectOfType<LevelManager>();
        _levelManager.StartLevel();
    } // InitGame


    /// update system
    public void RegisterUpdate(MonoBehaviourUpdate newObj) {
        _updates.Add(newObj);
    } // AddUpdate

    public void UnRegisterUpdate(MonoBehaviourUpdate newObj) {
        _updates.Remove(newObj);
    } // AddUpdate

    public void Update() {

        if (_levelManager.IsLevelPlaying()) {
            float deltaTime = Time.deltaTime;
            for (int i = _updates.Count - 1; i >= 0; i--) {
                _updates[i].UpdateMethod(deltaTime);
            }
        }
    } // Update

    // gets
    public SceneManager GetSceneManager() {
        return _sceneManager;
    } // GetSceneManager

    public GameData GetGameData() {
        return _gameData;
    } // GetGameData

    public LevelManager GetLevelManager() {
        return _levelManager;
    } // GetLevelManager


    /////// Singleton
    public static GameManager GetPtr() {
        if (_instance == null)
            _instance = FindObjectOfType<GameManager>();

        return _instance;
    } // GetPtr
}
