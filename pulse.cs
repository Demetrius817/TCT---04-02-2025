using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pulse : MonoBehaviour {

    public GameObject buttonImage;
    public float transparency;
    public float changeSpeed;


    void Update() {
        buttonImage.GetComponent<Renderer>().material.color = new Color(1, 1, 1, transparency);
        float pingpong = Mathf.PingPong((Time.time) * changeSpeed, 1);
        transparency = Mathf.Lerp(.3f, 1f, pingpong);
    }

}
