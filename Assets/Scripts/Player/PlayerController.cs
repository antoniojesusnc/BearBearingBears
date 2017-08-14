using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourUpdate {

    public float _pushStrength;
    private int _timesPushSameDirection;
    private EDirectionsPositions _pushDirection;

    private PlayerState _playerState;
    private BearController _bearBeingPushed;

    private GameData _gameData;

    private float _timeStamp;
    // Use this for initialization
    void Start() {
        _gameData = GameManager.GetPtr().GetGameData();
        _playerState = GetComponent<PlayerState>();
        _playerState.OnNewPush += OnPush;
    } // Start

    private void OnPush(EDirectionsPositions direction, bool isNewDirection) {
        if (isNewDirection) {
            _timesPushSameDirection = 1;
        } else {
            ++_timesPushSameDirection;
        }

        _pushStrength = _gameData.GetPlayerStrength(_timesPushSameDirection);
        _pushDirection = direction;

        transform.position = GameManager.GetPtr().GetLevelManager().Push(_pushDirection, _pushStrength);
        _timeStamp = 0;
    } // OnChangeState

    public override void UpdateMethod(float deltaTime) {
        if (_bearBeingPushed != null) {
            UpdatePlayerPosition();
        }

        _timeStamp += deltaTime;
        if (_timeStamp > _gameData.PlayerResetValueAfterSeconds) {
            _timeStamp = 0;
            _timesPushSameDirection = 0;
        }
    } // UpdateMethod

    private void UpdatePlayerPosition() {
        Vector3 offset = Vector3.zero;
        transform.position = _bearBeingPushed.transform.position + offset;
    } // UpdatePlayerPosition

    public void SetPushingItem(BearController bear) {
        _bearBeingPushed = bear;
    } // SetPushingItem
}
