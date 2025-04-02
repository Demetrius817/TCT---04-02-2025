using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] public float minYRotation = 10;
    [SerializeField] public float maxYRotation = 10;
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;
    [SerializeField] private float xmin;
    [SerializeField] private float xmax;
    private float mouseX;
    private float mouseY;

    private Quaternion rotation;

    private void Start() {
        rotation = transform.localRotation;
    }
    private void Update() {
        
            // Get mouse input
            mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
            mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

            mouseX = Mathf.Clamp(mouseX, xmin, xmax);
            // calculate target rotation
            Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

            Quaternion targetRotation = rotationX * rotationY;


            // rotate
            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, targetRotation, smooth * Time.deltaTime);
        
    }

}
