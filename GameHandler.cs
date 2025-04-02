using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameHandler : MonoBehaviour {

    private PlayerInputActions _inputActions;

   [SerializeField] PlayerCheckPoint TestStageScript;
   [SerializeField] PlayerDeath MainStageScript;
    public static GameHandler Instance { get; private set; }

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameWon,
        GameLost,

    }

    private State state;
    private float countdownToStartTimer = 2f;

    private void Awake() {
        Instance = this;
       // state = State.WaitingToStart;
       // state = State.GamePlaying;
    }

    public void Start() {
        _inputActions = new PlayerInputActions();
        if (_inputActions == null) {
            Debug.Log("Input Actions Is Null!");
        } else {
            _inputActions.Player.Enable();
        }
         state = State.WaitingToStart;
       // state = State.GamePlaying;


    }


    private void Update() {
        if (MainStageScript.enabled == true) {

            switch (state) {
                case State.WaitingToStart:
                    if (_inputActions.Player.Jump.triggered && state == State.WaitingToStart) {
                        state = State.CountdownToStart;
                    }
                    break;
            }
            switch (state) {
                case State.CountdownToStart:
                    countdownToStartTimer -= Time.deltaTime;
                    if (countdownToStartTimer < 0f) {
                        state = State.GamePlaying;
                    }
                    break;
            }
            switch (state) {
                case State.GamePlaying:
                    if (EndGameTrigger.Instance.isEndGameHit()) {
                        state = State.GameWon;
                    }
                    break;
                case State.GameWon:
                    break;
            }
        }
            if (TestStageScript.enabled == false) {
                switch (state) {
                    case State.GamePlaying:
                        if (PlayerDeath.Instance.isPlayerDead()) {
                            state = State.GameLost;
                        }
                        break;
                    case State.GameLost:
                        break;
                }
            }
            Debug.Log(state);

        }
    

    public bool isGameWaitingToStart() {
        return state == State.WaitingToStart;
    }
    public bool isCountingDownToStart() {
        return state == State.CountdownToStart;
    }
    public bool isGamePlaying() {
        return state == State.GamePlaying;
    }
    public bool isGameWon() {
        return state == State.GameWon;
    }
    public bool isGameLost() {
        return state == State.GameLost;
    }
  
}
