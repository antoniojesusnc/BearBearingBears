using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour {

    public EDirectionsPositions _roadPosition;
    private Vector3 _initialPosition;

    private BearController _bearOnRoad;

    public void Awake() {
        _initialPosition = transform.Find("InitialBearPosition").transform.position;
    } // Start

    public void StartRoad() {
        gameObject.SetActive(true);
        _bearOnRoad = GetComponentInChildren<BearController>();
        _bearOnRoad.transform.position = _initialPosition;
    } // StartRoad

    public void FinishRoad() {
        gameObject.SetActive(false);
    } // FinishRoad

    /// <summary>
    /// Push the bear and return the new Bear Position
    /// </summary>
    /// <param name="pushStrength"></param>
    /// <returns></returns>
    public Vector3 PushBear(float pushStrength) {
        Vector3 pushDirection = GameData.EDirectiosPositionsToVector3(GameData.GetOpositeDirection(_bearOnRoad.getDirection()));
        _bearOnRoad.transform.position += pushDirection * pushStrength;

        ClampBearPos();

        return _bearOnRoad.transform.position;
    } // PushBear

    private void ClampBearPos() {
        switch (_roadPosition) {
            case EDirectionsPositions.Left:
            if (_bearOnRoad.transform.position.x < _initialPosition.x)
                _bearOnRoad.transform.position = _initialPosition;
            break;
            case EDirectionsPositions.Right:
            if (_bearOnRoad.transform.position.x > _initialPosition.x)
                _bearOnRoad.transform.position = _initialPosition; break;
            case EDirectionsPositions.Top:
            if (_bearOnRoad.transform.position.y > _initialPosition.y)
                _bearOnRoad.transform.position = _initialPosition; break;
            case EDirectionsPositions.Bot:
            if (_bearOnRoad.transform.position.y < _initialPosition.y)
                _bearOnRoad.transform.position = _initialPosition; break;
        }
    } // ClampBearPos

    // gets & sets
    public BearController GetBear() {
        return _bearOnRoad;
    } // GetBear

    public EDirectionsPositions GetRoadPosition() {
        return _roadPosition;
    } // GetRoadPosition
}
