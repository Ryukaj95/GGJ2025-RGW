using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : Singleton<SpawnPoint>
{
    public Transform GetPosition()
    {
        return this.transform;
    }
}
