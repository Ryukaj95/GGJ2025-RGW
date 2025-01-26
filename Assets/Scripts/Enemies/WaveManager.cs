using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{

    [SerializeField] private int currentWaveIndex = 0;

    public WaveData CurrentWave => waves[currentWaveIndex];
    [SerializeField] private bool started;

    [SerializeField] public float timer = 300;

    private List<EnemyData> spawnQueue = new List<EnemyData>();

    [SerializeField] public List<WaveData> waves;

    [SerializeField] private List<EnemyController> currentEnemies = new List<EnemyController>();

    [SerializeField] private bool isEndless = false;

    [SerializeField] public bool wavesFinished = false;

    public void NextWave()
    {
        StageManager.Instance.stopShooting = false;
        currentWaveIndex++;
        if (currentWaveIndex < waves.Count)
        {
            spawnQueue.AddRange(CurrentWave.enemies);
            timer = CurrentWave.timer;
            StartWave();
        }
        else
        {
            wavesFinished = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            timer -= Time.deltaTime;

            if (timer <= 0 && !CurrentWave.isBossWave && !isEndless)
            {
                EndWave();
            }
        }
    }

    public void StartWave()
    {
        spawnQueue.AddRange(CurrentWave.enemies);
        timer = CurrentWave.timer;
        started = true;
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        while (started && spawnQueue.Count > 0)
        {
            if (CurrentWave.isRandom)
            {
                int randomIndex = UnityEngine.Random.Range(0, spawnQueue.Count);
                EnemyData chosenEnemy = spawnQueue[randomIndex];
                EnemyController enemy = Instantiate(chosenEnemy.enemyPrefab).GetComponent<EnemyController>();
                enemy.transform.position = chosenEnemy.isRandom ? GetRandomPointOfCollider(StageManager.Instance.spawnAreas[UnityEngine.Random.Range(0, StageManager.Instance.spawnAreas.Count)])
                                                                : GetRandomPointOfCollider(StageManager.Instance.spawnAreas[chosenEnemy.spawnArea]);
                currentEnemies.Add(enemy);
                if (!CurrentWave.isLooping)
                {
                    spawnQueue.RemoveAt(randomIndex);
                }
                yield return new WaitForSeconds(CurrentWave.spawnRate);
            }
            else
            {
                foreach (var toSpawn in spawnQueue)
                {
                    EnemyController enemy = Instantiate(toSpawn.enemyPrefab).GetComponent<EnemyController>();
                    enemy.transform.position = toSpawn.isRandom ? GetRandomPointOfCollider(StageManager.Instance.spawnAreas[UnityEngine.Random.Range(0, StageManager.Instance.spawnAreas.Count)])
                                                                : GetRandomPointOfCollider(StageManager.Instance.spawnAreas[toSpawn.spawnArea]);
                    currentEnemies.Add(enemy);
                    yield return new WaitForSeconds(CurrentWave.spawnRate);
                }
                if (!CurrentWave.isLooping)
                {
                    spawnQueue.Clear();
                }
            }
        }
        yield break;
    }

    public void EndWave()
    {
        started = false;
        if (currentEnemies.Count > 0)
        {
            FleeEnemies();
        }
        else
        {
            NextWave();
        }
    }

    public void FleeEnemies()
    {
        StageManager.Instance.stopShooting = true;
        foreach (var enemy in currentEnemies)
        {
            enemy.flee = true;
        }
        NextWave();
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        currentEnemies.Remove(enemy);
        if (spawnQueue.Count == 0 && currentEnemies.Count == 0 && !CurrentWave.isLooping)
        {
            EndWave();
        }
    }

    public void Reset()
    {
        currentWaveIndex = 0;
        started = false;
        timer = 300;
        spawnQueue = new List<EnemyData>();
        isEndless = false;
        wavesFinished = false;
    }

    public void SpawnEnemy(GameObject enemyObj, Vector2 spawnPosition, EnemyData enemyData)
    {
        EnemyController enemy = Instantiate(enemyObj).GetComponent<EnemyController>();
        enemy.transform.position = spawnPosition;

        Collider2D landArea = enemyData.isRandom ? StageManager.Instance.landingAreas[UnityEngine.Random.Range(0, StageManager.Instance.landingAreas.Count)]
                                                 : StageManager.Instance.landingAreas[enemyData.landingArea];

        enemy.Land(GetRandomPointOfCollider(landArea));
        currentEnemies.Add(enemy);
    }

    public List<GameObject> GetEnemiesOfCurrentWave()
    {
        List<GameObject> enemies = new List<GameObject>();
        CurrentWave.enemies.ForEach(enemy => enemies.Add(enemy.enemyPrefab));
        return enemies;
    }

    public Vector3 GetRandomPointOfCollider(Collider2D collider)
    {
        return new Vector3(
            UnityEngine.Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            UnityEngine.Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            UnityEngine.Random.Range(collider.bounds.min.z, collider.bounds.max.z));
    }

}
