using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroyedParticles : MonoBehaviour {
    [SerializeField] private GameObject target;
    [SerializeField] private ParticleSystem yourParticleSystem;
    private bool Played = false;


    private void Update() {
        if (target == null && Played == false) {
            yourParticleSystem.Play();
            Played = true;
        }
    }
}
