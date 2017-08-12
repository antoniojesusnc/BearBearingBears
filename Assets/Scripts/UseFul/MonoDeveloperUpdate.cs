using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoBehaviourUpdate : MonoBehaviour {

    void Awake() {
        GameManager.GetPtr().RegisterUpdate(this);

        NewAwake();
    } // Awake

    public virtual void NewAwake() { }

    public abstract void UpdateMethod(float deltaTime);
}
