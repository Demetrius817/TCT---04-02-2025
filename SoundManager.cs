using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundType {
    Running,
    ShootPocketPistol,
    TargetDestroyed,
    MenuMusic
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [Header("Audio Reference")]
    [SerializeField] private AudioClip[] soundList;

    public static SoundManager instance { get; private set; }
    private AudioSource audioSource;
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1) {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

}
