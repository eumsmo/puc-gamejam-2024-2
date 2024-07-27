using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public Actions inputActions;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }


    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        inputActions = new Actions();
        inputActions.Player.Enable();
    }
}
