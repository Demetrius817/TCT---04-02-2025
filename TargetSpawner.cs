using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Targets;
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other) {
        foreach (var target in Targets) {
            target.SetActive(true);
        }
    }

  //  private void OnTriggerExit(Collider other) {

 //       onTriggerExit.Invoke();

 //   }

}
