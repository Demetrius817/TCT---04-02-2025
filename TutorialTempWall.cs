using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTempWall : MonoBehaviour
{

    [SerializeField] private GameObject Player;

    public void OnTriggerEnter(Collider other) {
        if (Player) {
            Destroy(this.gameObject);
            Debug.Log("WallBroken");

        }

    }
}
