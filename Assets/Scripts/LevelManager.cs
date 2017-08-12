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

    private LevelState _levelState;
    private float _levelStartCountDown;
    private Dictionary<EDirectionsPositions, RoadController> _roadsByPosition;

    private HoneyController _honeyController;
    private PlayerController _playerController;

    private void Awake() {
        _roadsByPosition = new Dictionary<EDirectionsPositions, RoadController>();
        _honeyController = FindObjectOfType<HoneyController>();
        _playerController = FindObjectOfType<PlayerController>();
    } // _roadsByPosition 

    public void StartLevel() {
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
        if (_roadsByPosition.ContainsKey(pushDirection)) {
            RoadController roadController = _roadsByPosition[pushDirection];
            _playerController.SetPushingItem(roadController.GetBear());

            return roadController.PushBear(pushStrength);
        } else {
            _playerController.SetPushingItem(null);
            return GetTemporalPositionForPlayer(pushDirection);
        }
    } // Push

    private Vector3 GetTemporalPositionForPlayer(EDirectionsPositions pushDirection) {
        Debug.Log("set position close to honey depending of the pushDirection");
        return _honeyController.transform.position;
    } // GetTemporalPositionForPlayer

    // road and push managemens
    public void RegisterRoad(EDirectionsPositions roadPosition, RoadController roadController) {
        _roadsByPosition.Add(roadPosition, roadController);
    } // RegisterRoad

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
