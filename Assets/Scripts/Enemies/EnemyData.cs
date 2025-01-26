using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] public GameObject enemyPrefab;
    [Range(0, 4)]
    [SerializeField] public int landingArea = 0;
    [Range(0, 2)]
    [SerializeField] public int spawnArea = 0;

    [SerializeField] public bool isRandom = false;
}
