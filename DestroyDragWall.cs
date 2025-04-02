using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDragWall : MonoBehaviour
{
    private HitWall destroyWall;
    public void Update() {
        if (destroyWall == true) {
            Destroy(gameObject);
            Debug.Log("I HIT A WALL");

        }
    }

}
