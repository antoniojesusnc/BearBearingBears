using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDirectionsPositions {
    Top,
    Bot,
    Left,
    Right,
    Size
} // EDirectiosPositions

public class GameData : MonoBehaviour {

    [Header("Level Data")]
    public int LevelStartCountDownSeconds;
    public float HoneyRadious;

    [Header("Bear Data")]
    public float InitialBearSpeed;

    [Header("Player Data")]
    public float MinPlayerPushStrength;



    public float GetPlayerStrength(float timeWithSameDirection) {
        // I have to find a equation for reward same direccion pushing
        return MinPlayerPushStrength;

        /// static methods
    } // GetPlayerStrength

    public static EDirectionsPositions GetOpositeDirection(EDirectionsPositions directionOrPoint) {
        switch (directionOrPoint) {
            case EDirectionsPositions.Top: return EDirectionsPositions.Bot;
            case EDirectionsPositions.Bot: return EDirectionsPositions.Top;
            case EDirectionsPositions.Left: return EDirectionsPositions.Right;
            case EDirectionsPositions.Right: return EDirectionsPositions.Left;
        }
        Debug.LogWarning("Calling GetOpositeDirection with No Valid Value" + directionOrPoint);
        return EDirectionsPositions.Size;
    } // EDirectiosPositionsToVector3


    public static Vector3 EDirectiosPositionsToVector3(EDirectionsPositions directionOrPoint) {
        switch (directionOrPoint) {
            case EDirectionsPositions.Top: return Vector3.up;
            case EDirectionsPositions.Bot: return Vector3.down;
            case EDirectionsPositions.Left: return Vector3.left;
            case EDirectionsPositions.Right: return Vector3.right;
        }
        Debug.LogWarning("Calling EDirectiosPositionsToVector3 with No Valid Value" + directionOrPoint);
        return Vector3.zero;
    } // EDirectiosPositionsToVector3
}
