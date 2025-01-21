using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPoint : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    private float bulletSpeed = 4f;

    public void Shoot(Vector2 target) {
        if (!bulletPrefab) return;

        Bullet bullet = Instantiate(bulletPrefab, this.gameObject.transform.position, Quaternion.identity);
        // bullet.SetTarget(target);
        bullet.SetDirection(new Vector2(1, 0));
        bullet.SetIsFriendlyToPlayer(false);
        bullet.SetSpeed(bulletSpeed);
    }
}
