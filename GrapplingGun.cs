using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class GrapplingGun : MonoBehaviour {
    /*
    private LineRenderer lr;
    */
    private Vector3 grapplePoint;
    public LayerMask WhatIsGrappable;
    private SpringJoint joint;

    [Header("References")]
    [SerializeField] public Transform GrappleMuzzle;
    [SerializeField] public Transform Cam;
    [SerializeField] public Transform Player;
    private WeaponSwitching ws;

    [Header("GrappleGun")]
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private float springN = 4.5f;
    [SerializeField] private float damperN = 7f;
    [SerializeField] private float massScaleN = 4.5f;

    [SerializeField] private float DistFromPointMin = 0.25f;
    [SerializeField] private float DistFromPointMax = 0.8f;

    /*
    private void Awake() {
        lr = GetComponent<LineRenderer>();
    }
    */
    private void Start() {
        ws = GetComponent<WeaponSwitching>();
    }

    private void OnDisable() => StopGrapple();


    private void Update() {


        if (Input.GetMouseButtonDown(0) && this.gameObject.activeSelf) {
            StartGrapple();
        } else
        if (Input.GetMouseButtonUp(0)) {
            StopGrapple();

        }

    }

    //Called after update.
    /*
    private void LateUpdate() {

        DrawRope();

    }
    */
    private void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(Cam.position, Cam.forward, out hit, maxDistance, WhatIsGrappable)) {
            grapplePoint = hit.point;
            joint = Player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(Player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point.
            joint.maxDistance = distanceFromPoint * DistFromPointMax;
            joint.minDistance = distanceFromPoint * DistFromPointMax;

            //Change these to fit game!
            joint.spring = springN;
            joint.damper = damperN;
            joint.massScale = massScaleN;
            /*
            lr.positionCount = 2;
            */
            PGrappleWeaponAnimation.Instance.Wanimator.SetBool(PGrappleWeaponAnimation.Instance.isShootingGrappleHash, true);

        }
    }
    /*
    private void DrawRope() {
        //If not grapple.. don't draw rope.
        if (!joint) return;

        lr.SetPosition(0, GrappleMuzzle.position);
        lr.SetPosition(1, grapplePoint);
    }
    */
    private void StopGrapple() {
        /*
        lr.positionCount = 0;
        */
        Destroy(joint);
        PGrappleWeaponAnimation.Instance.Wanimator.SetBool(PGrappleWeaponAnimation.Instance.isShootingGrappleHash, false);

    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
