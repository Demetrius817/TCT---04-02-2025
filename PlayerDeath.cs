using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {
    public static PlayerDeath Instance { get; private set; }

    public bool hitPlayerDeath = false;

    private void Start() {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("DeathSpace") || other.gameObject.CompareTag("isMonster")) {
            hitPlayerDeath = true;

            Debug.Log("Dead");
        }
    }
    public bool isPlayerDead() {
        return hitPlayerDeath == true;
    }

}
