using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWall : MonoBehaviour {

    [SerializeField] private GameObject meWall;

    //goes on ball for laser


    private void OnCollisionEnter(Collision collision) {
            if(collision.transform.CompareTag("HitWall")) {

            Destroy(meWall);
            Debug.Log("I HIT A WALL");

        }
    }

}
