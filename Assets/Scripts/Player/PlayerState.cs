using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public delegate void OnStateChange(EDirectionsPositions newState, bool isNewState);
    public event OnStateChange OnNewPush;

    private EDirectionsPositions _currentPushState;

    public void NewInput(EDirectionsPositions newState) {
        bool isNewState = false;

        if (newState != _currentPushState) {
            _currentPushState = newState;
            isNewState = true;
        }

        OnNewPush(_currentPushState, isNewState);
    } // SetState
}
