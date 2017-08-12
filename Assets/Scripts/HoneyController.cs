using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyController : MonoBehaviourUpdate {

    private List<Transform> _bearPositions;
    public float _radious;

    private void Start() {
        _radious = GameManager.GetPtr().GetGameData().HoneyRadious;

        _bearPositions = new List<Transform>();
        foreach (var bear in FindObjectsOfType<BearController>()) {
            _bearPositions.Add(bear.transform);
        }

    } // Start

    public override void UpdateMethod(float deltaTime) {
        for (int i = _bearPositions.Count - 1; i >= 0; i--) {
            if (( _bearPositions[i].position - transform.position ).magnitude < _radious) {
                Debug.LogWarning("End game");
            }
        }
    } // UpdateMethod
}
