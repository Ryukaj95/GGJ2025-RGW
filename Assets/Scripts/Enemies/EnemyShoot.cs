using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private int firedBulletsPerSeconds = 3;
    [SerializeField] private int magazineSize = 6;
    [SerializeField] private float reloadTimeInSeconds = 3f;

    private bool isMagazineEmpty => currentBulletsInMagazine <= 0;
    private GameObject shootTarget => PlayerController.Instance.gameObject;
    
    private int currentBulletsInMagazine;
    private ShootPoint shootPoint;
    private bool isShootingInCD, isReloading = false;

    private void Awake() {
        shootPoint = GetComponentInChildren<ShootPoint>();

        currentBulletsInMagazine = magazineSize;
    }

    private void Update() {
        // If the magazine is empty and it's not reloading
        // start the reload routine
        if (isMagazineEmpty && !isReloading) {
            StartCoroutine(ReloadRoutine());
            return;
        }

        // If the shoot is in cooldown or it's reloading its magazine
        // don't do anything
        if (!isShootingInCD && !isReloading && !isMagazineEmpty) {
            Shoot();
            return;
        };
    }

    private void Shoot() {
        if (!shootPoint) return;

        // Shoot
        shootPoint.Shoot(shootTarget.transform.position);
        currentBulletsInMagazine -= 1;

        StartCoroutine(ShootCDRoutine());
    }

    private IEnumerator ShootCDRoutine() {
        isShootingInCD = true;
        yield return new WaitForSeconds(1f / firedBulletsPerSeconds);
        isShootingInCD = false;
    }

    private IEnumerator ReloadRoutine() {
        isReloading = true;
        yield return new WaitForSeconds(reloadTimeInSeconds);
        isReloading = false;
        currentBulletsInMagazine = magazineSize;
    }
}
