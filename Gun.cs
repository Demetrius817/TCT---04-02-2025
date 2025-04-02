using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private float maxDistance = 200;
    [SerializeField] private float GunShotVolume = 0.02f;
    public LayerMask mask;
    public LayerMask masktwo;
    public static Gun Instance { get; private set; }

    [Header("References")]
    [SerializeField] public GunData gunData;
    [SerializeField] private Transform cam;
    private UI_Manager uiManager;
    [SerializeField] private ParticleSystem yourParticleSystem;




    float timeSinceLastShot;
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        gunData.currentAmmo = gunData.magSize;

    }


    private void OnEnable()
    {
        PlayerShoot.shootInput += Shoot;
    }

    private void OnDisable()
    {
        PlayerShoot.shootInput -= Shoot;
        gunData.reloading = false;
    }
   

    public void StartReload()
    {
         if(!gunData.reloading && this.gameObject.activeSelf) {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;
        uiManager.UpdateAmmo(gunData.currentAmmo);

        gunData.reloading = false;
    }

    public bool CanShoott() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        if (GameHandler.Instance.isGamePlaying()) {

            if (gunData.currentAmmo > 0) {

                if (CanShoott()) {
                    SoundManager.PlaySound(SoundType.ShootPocketPistol, GunShotVolume);
                    PPistolWeaponAnimation.Instance.Wanimator.SetTrigger("TrigFire");
                    yourParticleSystem.Play();
                    if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance, masktwo)) {

                        IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                        damageable?.TakeDamage(gunData.damage);
                        // Debug.Log("Gun1");

                    } else
                    if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, gunData.maxDistance, mask)) {
                        if (hit.transform.gameObject.tag == "IsWallOfTarget") {
                            Monster.Instance.MissMonsterMove();
                            Debug.Log("worked");
                        }
                    }

                    gunData.currentAmmo--;
                    uiManager.UpdateAmmo(gunData.currentAmmo);
                    timeSinceLastShot = 0;

                }
            }
        }
    }

    private void Update() {

        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);

    }


}

