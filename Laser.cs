using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Laser : MonoBehaviour {
    //this script goes on weapon (laser to hold ball like osu)
    [Header("References")]
    [SerializeField] private Transform cam;
    [SerializeField] private float maxDistance = 200;
    [SerializeField] private KeyCode ShootLaser;
    private bool gotObject = false;
    public LayerMask mask;
    public LayerMask maskk;

    Vector3 dist;
    Vector3 startPos;
    private DraggableObject currentDraggedObject;

    private void Update() {
        RaycastHit hit;
        if (Input.GetKeyDown(ShootLaser)) {

            if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, mask)) {
                if (hit.transform.gameObject.tag == "CanDrag") {

                    currentDraggedObject = hit.transform.gameObject.GetComponent<DraggableObject>();
                    currentDraggedObject.isBeingDragged = true;
                    gotObject = true;
                    Debug.Log(hit.transform.name);
                    Debug.DrawRay(cam.position, cam.forward * maxDistance);

                }

            }
        }
        if (currentDraggedObject != null)

            if (gotObject == true) {
                if (currentDraggedObject.isBeingDragged == true) {
                    startPos = Camera.main.WorldToScreenPoint(currentDraggedObject.transform.position);
                    //Get the point that is clicked
                    Vector3 hitPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPos.z); ;
                    //Move your cube GameObject to the point where you clicked
                    currentDraggedObject.transform.position = Camera.main.ScreenToWorldPoint(hitPoint);
                }
            }
        if (gotObject == true) {
            if (Input.GetKeyUp(ShootLaser) || Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, maskk)) {
                currentDraggedObject.isBeingDragged = false;
                gotObject = false;
            }
        }
    }



}