using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class MenuLookAround : MonoBehaviour {
    public float rotMult = 4f;

    float yaw = 0f;
    float pitch = 0f;

    public float maxY = -65; // For some reason, the signs are strange.
    public float minY = 50;
    public float maxX = 20;
    public float minX = 50;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        yaw += rotMult * Input.GetAxis("Mouse X");
        pitch -= rotMult * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, maxY, minY); // Clamp viewing up and down
        yaw = Mathf.Clamp(yaw, maxX, minX);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}