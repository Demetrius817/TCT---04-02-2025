using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheckPoint : MonoBehaviour {
    private PlayerCheckPoint PCP;
    [SerializeField] private GameObject Player;

    [SerializeField] private GameObject checkPoint;


    private void Start() {
        PCP = GetComponent<PlayerCheckPoint>();

    }
    private void Update() {

    }

    public void OnTriggerEnter(Collider other) {
        if (Player) {
            PlayerCheckPoint.Instance.spawnPoint = checkPoint.transform.position;
            Debug.Log("NewSpawnSet");   

        }

    }
}
