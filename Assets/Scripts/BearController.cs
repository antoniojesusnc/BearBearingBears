using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviourUpdate {

    public EDirectionsPositions _direction;
    private Vector3 _directionVector;
    private float _speed;

    private float _timeBearMoving;

    private GameData _gameData;

    public override void NewAwake() {
        _gameData = GameManager.GetPtr().GetGameData();
        _speed = _gameData.BearInitialSpeed;
        _directionVector = GameData.EDirectiosPositionsToVector3(_direction);
    } // NewAwake

    public override void UpdateMethod(float deltaTime) {
        transform.Translate(_speed * _directionVector * deltaTime);

        _timeBearMoving += deltaTime;
        _speed = _gameData.GetBearSpeed(_timeBearMoving);
    } // UpdateMethod

    // gets & sets
    public void SetSpeed(float speed) {
        _speed = speed;
    } // SetSpeed

    public EDirectionsPositions getDirection() {
        return _direction;
    } // getDirection
}
