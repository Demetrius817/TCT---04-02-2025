using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddOn : MonoBehaviour
{
    //this script goes on projectile
    [Header("References")]
    public GameObject Player;
    private Rigidbody rb;

    [Header("GrappleGun")]
    [SerializeField] public float destroyTime = 1;
    public KeyCode throwKey = KeyCode.Mouse0;
    public KeyCode TPKey = KeyCode.Mouse1;

    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
         if (Input.GetKeyDown(TPKey)) {
           
            Debug.Log("TP");
        }
        Destroy(gameObject, destroyTime);
        if (Input.GetKeyDown(throwKey)) {
            Destroy(gameObject);
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (targetHit)
            return;
        else
            Player = GameObject.Find("Player");
        Player.transform.position = gameObject.transform.position;
        targetHit = true;
        Destroy(gameObject);
        
    }

}
