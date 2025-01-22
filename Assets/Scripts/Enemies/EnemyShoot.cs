using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private int firedBulletsPerSeconds = 3;
    [SerializeField] private int magazineSize = 7;
    [SerializeField] private float reloadTimeInSeconds = 3f;
    [SerializeField] private Transform[] spawnPoints = {};

    [System.Serializable]
    private class BulletSpreadSettings {
        [SerializeField] public bool isActive = false;

        [SerializeField] public float angleSpread = 360f;
        [SerializeField] public int bulletsPerSpread = 36;
        [SerializeField] public float firerate = 1f;
    }

    [SerializeField] private BulletSpreadSettings bulletSpreadSettings;

    private bool isMagazineEmpty => currentBulletsInMagazine <= 0;
    private GameObject shootTarget => PlayerController.Instance.gameObject;
    
    private int currentBulletsInMagazine;
    private ShootPoint[] shootPoints = {};
    private bool isShootingInCD, isReloading = false;

    private void Awake() {
        shootPoints = GetComponentsInChildren<ShootPoint>();

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
        if (shootPoints.Length == 0) return;

        if (bulletSpreadSettings.isActive) {
            StartCoroutine(ShootSpreadRoutine());
            return;
        }

        // Shoot
        StartCoroutine(ShootRoutine());
        return;
    }

    private IEnumerator ShootSpreadRoutine() {
        isShootingInCD = true;

        Vector2 directionToPlayer = (shootTarget.transform.position - transform.position).normalized;

        float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        float angleStep = bulletSpreadSettings.angleSpread / bulletSpreadSettings.bulletsPerSpread;

        float startAngle = baseAngle - (angleStep * (bulletSpreadSettings.bulletsPerSpread - 1) / 2);

        for (int i = 0; i < bulletSpreadSettings.bulletsPerSpread; i++) {
            float angle = startAngle + (i * angleStep);

            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            // shootPoints[0].ShootToDirection(direction);

            foreach (ShootPoint shootPoint in shootPoints) {
                shootPoint.ShootToDirection(direction.normalized);
            }
        }

        yield return new WaitForSeconds(bulletSpreadSettings.firerate);
        isShootingInCD = false;
    }

    private IEnumerator ShootRoutine() {
        isShootingInCD = true;

        foreach (ShootPoint shootPoint in shootPoints) {
            if (!isMagazineEmpty) {
                shootPoint.ShootToTarget(shootTarget.transform.position);
                currentBulletsInMagazine -= 1;
            }
        }

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
