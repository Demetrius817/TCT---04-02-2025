using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour {

  // [SerializeField] private GameObject checkPoint;
   public Vector3 spawnPoint;
   public Vector3 singlespawnPoint;
    public static PlayerCheckPoint Instance { get; private set; }


    private void Awake() {
        Instance = this;
    }
        private void Start() {
        spawnPoint = gameObject.transform.position;
        singlespawnPoint = gameObject.transform.position;
    }

    private void Update() {
        // reset
        if (Input.GetKeyDown(KeyCode.F)) {
            gameObject.transform.position = spawnPoint;
        }
        // full reset
        if (Input.GetKeyDown(KeyCode.G)) {
            gameObject.transform.position = singlespawnPoint;
        }
    }

    private void OnTriggerEnter(Collider other) {
     //   if (other.gameObject.CompareTag("CheckPoint")) {
    //        spawnPoint = checkPoint.transform.position;
     //       Debug.Log("NewSpawn");
     //   }
        if (other.gameObject.CompareTag("DeathSpace")) {
            gameObject.transform.position = spawnPoint;
        }

        
    }

}
