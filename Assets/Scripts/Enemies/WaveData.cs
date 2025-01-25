using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Enemies/Wave")]
public class WaveData : ScriptableObject
{
    [SerializeField] public bool isRandom;
    [SerializeField] public bool isLooping;
    [SerializeField] public float spawnRate = 1.5f;
    [SerializeField] public List<GameObject> enemies;

    [SerializeField] public bool isBossWave;
    [SerializeField] public float timer = 300;
}

