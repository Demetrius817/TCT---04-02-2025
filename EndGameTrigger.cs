using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndGameTrigger : MonoBehaviour {
    public static EndGameTrigger Instance { get; private set; }

    public bool hitEndGame = false;

    private void Start() {
        Instance = this;
    }

    public void OnTriggerEnter(Collider other) {

        hitEndGame = true;
    }


    public bool isEndGameHit() {
        return hitEndGame == true;
    }

    
}



