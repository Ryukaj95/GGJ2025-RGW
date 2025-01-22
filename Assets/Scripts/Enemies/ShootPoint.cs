using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootPoint : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    private float bulletSpeed = 4f;

    public void ShootToTarget(Vector2 target) {
        if (!bulletPrefab) return;

        Bullet bullet = CreateBullet();
        bullet.SetTarget(target);
    }

    public void ShootToDirection(Vector2 target) {
        if (!bulletPrefab) return;

        Bullet bullet = CreateBullet();
        bullet.SetDirection(target);
    }

    public Bullet CreateBullet() {
        Bullet bullet = Instantiate(bulletPrefab, this.gameObject.transform.position, Quaternion.identity);
        bullet.SetIsFriendlyToPlayer(false);
        bullet.SetSpeed(bulletSpeed);

        return bullet;
    }
}