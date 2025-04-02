using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [Header("References")]
    [SerializeField] WallRun wallRun; 

    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;
    [SerializeField] public float YPlayerStartDirectionY = 180f;
    [SerializeField] public float XPlayerStartDirectionX = 180f;

    [SerializeField] Transform cam = null;
     [SerializeField] public Transform orientation = null;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

   public float xRotation;
   public float yRotation;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yRotation = YPlayerStartDirectionY;
        xRotation = XPlayerStartDirectionX;
    }

    private void Update() {

        if (GameHandler.Instance.isGamePlaying()) {

            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");
        }


        yRotation += mouseX * sensX * multiplier;
            xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        


        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
       

    }

}
