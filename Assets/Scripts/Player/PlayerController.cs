using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourUpdate {

    public float _pushStrength;
    private float _timeWithSameDirection;
    private EDirectionsPositions _pushDirection;

    private PlayerState _playerState;
    private BearController _bearBeingPushed;

    // Use this for initialization
    void Start() {
        _playerState = GetComponent<PlayerState>();
        _playerState.OnNewPush += OnPush;
    } // Start

    private void OnPush(EDirectionsPositions direction, bool isNewDirection) {
        if (isNewDirection)
            _timeWithSameDirection = 0;

        _pushStrength = GameManager.GetPtr().GetGameData().GetPlayerStrength(_timeWithSameDirection);
        _pushDirection = direction;

        transform.position = GameManager.GetPtr().GetLevelManager().Push(_pushDirection, _pushStrength);
    } // OnChangeState

    public override void UpdateMethod(float deltaTime) {
        _timeWithSameDirection += deltaTime;
        if (_bearBeingPushed != null) {
            UpdatePlayerPosition();
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
