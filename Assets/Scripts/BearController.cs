using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviourUpdate {

    public EDirectionsPositions _direction;
    private Vector3 directionVector;

    private float _speed;

    public override void NewAwake() {
        _speed = GameManager.GetPtr().GetGameData().InitialBearSpeed;
        directionVector = GameData.EDirectiosPositionsToVector3(_direction);
    } // NewAwake


    public override void UpdateMethod(float deltaTime) {
        transform.Translate(_speed * directionVector * deltaTime);
    } // UpdateMethod

    // gets & sets
    public void SetSpeed(float speed) {
        _speed = speed;
    } // SetSpeed

    public EDirectionsPositions getDirection() {
        return _direction;
    } // getDirection
}
