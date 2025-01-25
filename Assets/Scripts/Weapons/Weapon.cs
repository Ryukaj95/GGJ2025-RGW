using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int firedBulletsPerSeconds = 3;
    [SerializeField] private int magazineSize = 7;
    [SerializeField] private float reloadTimeInSeconds = 3f;

    [SerializeField] public Bullet bulletPrefab;

    [SerializeField] private bool isFriendlyToPlayer = false;

    [SerializeField] private Transform[] spawnPoints;

    [System.Serializable]
    private class BulletSpreadSettings // Da fare Scriptable Objects
    {
        [SerializeField] public bool isActive = false;
        [SerializeField] public float angleSpread = 360f;
        [SerializeField] public int bulletsPerSpread = 36;
        [SerializeField] public float cooldown = 1f;
        [SerializeField] public float radius = 1f;

        [SerializeField] public float wait = 0f;
    }

    [SerializeField] private BulletSpreadSettings bulletSpreadSettings;

    private bool isMagazineEmpty => currentBulletsInMagazine <= 0;
    private GameObject shootTarget => PlayerController.Instance.gameObject; // quando ci sarà GameManager, sta a lui

    private int currentBulletsInMagazine;

    private bool isShootingInCD, isReloading = false;

    public bool playerTrigger = false;

    public bool active = true;

    private void Awake()
    {
        currentBulletsInMagazine = magazineSize;
    }

    private void Update()
    {
        if (!active) return;
        if (isMagazineEmpty && !isReloading)
        {
            StartCoroutine(ReloadRoutine());
            return;
        }
        if (!isFriendlyToPlayer || playerTrigger)
        {
            // If the shoot is in cooldown or it's reloading its magazine
            // don't do anything
            if (!isShootingInCD && !isReloading && !isMagazineEmpty)
            {
                Shoot();
                return;
            }
        }

    }

    private void Shoot()
    {
        if (spawnPoints.Length == 0 || StageManager.Instance.stopShooting) return;
        if (bulletSpreadSettings.isActive)
        {
            StartCoroutine(ShootSpreadRoutine());
            return;
        }

        // Shoot
        StartCoroutine(ShootRoutine());
        return;
    }

    private IEnumerator ShootSpreadRoutine()
    {
        isShootingInCD = true;

        Vector2 bulletDirection = GetBulletDirection(shootTarget.transform);

        float baseAngle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
        float angleStep = bulletSpreadSettings.angleSpread / bulletSpreadSettings.bulletsPerSpread;

        float startAngle = baseAngle - (angleStep * (bulletSpreadSettings.bulletsPerSpread - 1) / 2);

        for (int i = 0; i < bulletSpreadSettings.bulletsPerSpread; i++)
        {
            float angle = startAngle + (i * angleStep);

            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            Vector2 spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;

            Vector2 startPos = spawnPoint + (direction.normalized * bulletSpreadSettings.radius);


            Bullet bullet = BulletsManager.Instance.ShootBullet(startPos, direction, bulletPrefab, isFriendlyToPlayer);
            if (bulletSpreadSettings.wait > 0)
            {
                float pastSpeed = bullet.GetSpeed();
                bullet.SetSpeed(0);
                bullet.boostSettings.SetBoost(pastSpeed, 0f, bulletSpreadSettings.wait);
            }
        }

        yield return new WaitForSeconds(bulletSpreadSettings.cooldown);
        isShootingInCD = false;
    }

    private Vector2 GetBulletDirection(Transform playerTransform)
    {
        if (!isFriendlyToPlayer)
        {
            return (playerTransform.position - transform.position).normalized;
        }
        else
        {
            return Vector2.up;
        }
    }

    private IEnumerator ShootRoutine()
    {
        isShootingInCD = true;

        Vector3 playerPos = PlayerController.Instance.gameObject.transform.position;
        Vector2 source = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;
        Vector2 direction = !isFriendlyToPlayer ? (new Vector2(playerPos.x, playerPos.y) - source).normalized : Vector2.up;

        BulletsManager.Instance.ShootBullet(source, direction, bulletPrefab, isFriendlyToPlayer);
        currentBulletsInMagazine -= 1;

        yield return new WaitForSeconds(1f / firedBulletsPerSeconds);
        isShootingInCD = false;
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTimeInSeconds);

        isReloading = false;
        currentBulletsInMagazine = magazineSize;
    }
}
