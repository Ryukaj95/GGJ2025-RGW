using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : Singleton<BulletsManager>
{
    // Il sistema di proiettili deve permettere:

    // Creare i proiettili nel BulletManager
    // BulletManager.Instance.CreateEnemyBullet() -> Crea proiettile, popola con i parametri passati (prefabProiettile, posizione, direction, speed)
    // BulletManager.Instance.CreateFriendlyBullet()
    public void CreateEnemyBullet() { }
    public void CreateFriendlyBullet() { }
}
