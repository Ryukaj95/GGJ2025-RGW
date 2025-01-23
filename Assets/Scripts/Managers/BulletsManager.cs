using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BulletsManager : Singleton<BulletsManager>
{
    // Il sistema di proiettili deve permettere:

    public static readonly BulletsManager Instance = new BulletsManager();

    public static List<Bullet> enemyBulletsOnScreen = new List<Bullet>();

    // Creare i proiettili nel BulletManager
    // BulletManager.Instance.CreateEnemyBullet() -> Crea proiettile, popola con i parametri passati (prefabProiettile, posizione, direction, speed)
    // BulletManager.Instance.CreateFriendlyBullet()
    public static void CreateEnemyBullet(Vector2 spawnPoint, Vector2 direction, Bullet bullet)
    {
        Bullet newBullet = Instantiate(bullet, spawnPoint, Quaternion.identity);
        bullet.SetDirection(direction);
        bullet.SetIsFriendlyToPlayer(false);
        enemyBulletsOnScreen.Add(newBullet);
    }
    public void CreateFriendlyBullet() { }

    public static void RemoveBulletFromList(string bulletId)
    {
        enemyBulletsOnScreen.RemoveAt(enemyBulletsOnScreen.FindIndex(b => b.bulletId == bulletId));
    }
}
