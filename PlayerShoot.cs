using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("NewInput")]
    // New Input System
    private PlayerInputActions _inputActions;


    public static Action shootInput;
    public static Action reloadInput;

    private void Start() {
        _inputActions = new PlayerInputActions();
        if (_inputActions == null) {
            Debug.Log("Input Actions Is Null!");
        } else {
            _inputActions.Player.Enable();
        }

    }

    private void Update()
    {
        if (!GameHandler.Instance.isGamePlaying()) return;

        if (_inputActions.Player.Shoot.triggered) 
        
            shootInput?.Invoke();

            if (_inputActions.Player.Reload.triggered)
                reloadInput?.Invoke();
        
    }

}
