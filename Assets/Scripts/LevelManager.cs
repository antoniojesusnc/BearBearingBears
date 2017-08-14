using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState {
    NotStarted,
    InCountDown,
    Playing,
    Paused,
    Finish
} // LevelState 

public class LevelManager : MonoBehaviour {

    public List<RoadController> _roads;

    private LevelState _levelState;
    private float _levelStartCountDown;

    private HoneyController _honeyController;
    private PlayerController _playerController;

    private float _timeStamp;

    private GameData _gameData;

    private void Awake() {
        _gameData = GameManager.GetPtr().GetGameData();

        _honeyController = FindObjectOfType<HoneyController>();
        _playerController = FindObjectOfType<PlayerController>();
    } // _roadsByPosition 

    private void Update() {
        if (IsLevelPlaying()) {
            _timeStamp += Time.deltaTime;
            if (_timeStamp > _gameData.TimeForSecondRoad && !_roads[1].gameObject.activeSelf)
                _roads[1].StartRoad();

            if (_timeStamp > _gameData.TimeForThirdRoad && !_roads[2].gameObject.activeSelf)
                _roads[2].StartRoad();

            if (_timeStamp > _gameData.TimeForForthRoad && !_roads[3].gameObject.activeSelf)
                _roads[3].StartRoad();
        }
    } // Update

    public void StartLevel() {
        _timeStamp = 0;
        _roads[0].StartRoad();
        _roads[1].FinishRoad();
        _roads[2].FinishRoad();
        _roads[3].FinishRoad();

        startCountDown();
    } // StartLevel

    public void FinishLevel() {
        FinishGame();

        Debug.Log("Restarging Automathic");
        StartLevel();
    } // StartLevel

    private void startCountDown() {
        _levelStartCountDown = GameManager.GetPtr().GetGameData().LevelStartCountDownSeconds;

        StartCoroutine(countDownCo());
    } // startCountDown

    private IEnumerator countDownCo() {
        while (_levelStartCountDown > 0) {
            yield return 0;
            _levelStartCountDown -= Time.deltaTime;
        }

        StartGame();
    } // countDownCo

    private void StartGame() {
        _levelState = LevelState.Playing;
    } // StartGame

    private void FinishGame() {
        _levelState = LevelState.Finish;
    } // FinishGame

    /// <summary>
    /// If there are road in this direccion, call the road for push the bear and return the bear position
    /// otherwise return one position close to honey in the direccion.
    /// 
    /// In both scenario, call the player with the pushed object, bear if can or null otherwise
    /// </summary>
    /// <param name="pushDirection"></param>
    /// <param name="pushStrength"></param>
    /// <returns></returns>
    public Vector3 Push(EDirectionsPositions pushDirection, float pushStrength) {
        for (int i = 0; i < _roads.Count; i++) {
            if (_roads[i].GetRoadPosition() == pushDirection) {
                if (_roads[i].gameObject.activeSelf) {
                    _playerController.SetPushingItem(_roads[i].GetBear());
                    return _roads[i].PushBear(pushStrength);
                } else {
                    _playerController.SetPushingItem(null);
                    return GetTemporalPositionForPlayer(pushDirection);
                }
            }
        }
        _playerController.SetPushingItem(null);
        return GetTemporalPositionForPlayer(pushDirection);
    } // Push

    private Vector3 GetTemporalPositionForPlayer(EDirectionsPositions pushDirection) {
        Debug.Log("set position close to honey depending of the pushDirection");
        return _honeyController.transform.position;
    } // GetTemporalPositionForPlayer

    /// gets
    public bool IsLevelStarted() {
        return _levelState != LevelState.NotStarted;
    } // IsLevelStarted

    public bool IsLevelPlaying() {
        return _levelState == LevelState.Playing;
    } // IsLevelPlaying

    public bool IsLevelFinish() {
        return _levelState == LevelState.Finish;
    } // IsLevelFinish

}
