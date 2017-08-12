using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviourUpdate {

    private EDirectionsPositions _lastDirection;
    private EDirectionsPositions _newDirection;

    private PlayerState _playerState;

    public override void NewAwake() {
        base.NewAwake();

        _playerState = GetComponent<PlayerState>();
    } // NewAwake

    public override void UpdateMethod(float deltaTime) {
        _newDirection = EDirectionsPositions.Size;

        if (Input.anyKeyDown) {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                _newDirection = EDirectionsPositions.Left;
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                _newDirection = EDirectionsPositions.Right;
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                _newDirection = EDirectionsPositions.Bot;
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                _newDirection = EDirectionsPositions.Top;

            _playerState.NewInput(_newDirection);
        }
    } // UpdateMethod
}
