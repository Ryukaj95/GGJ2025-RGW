using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{

    [SerializeField] private int currentWaveIndex = 0;

    public WaveData CurrentWave => waves[currentWaveIndex];
    [SerializeField] private bool started;

    [SerializeField] public float timer = 300;

    private List<GameObject> spawnQueue = new List<GameObject>();

    [SerializeField] public List<WaveData> waves;

    [SerializeField] private List<EnemyController> currentEnemies = new List<EnemyController>();

    [SerializeField] private bool isEndless = false;

    void OnEnable()
    {
        spawnQueue.AddRange(CurrentWave.enemies);
        timer = CurrentWave.timer;
    }

    public void NextWave()
    {
        StageManager.Instance.stopShooting = false;
        if (currentWaveIndex < waves.Count)
        {
            currentWaveIndex++;
            spawnQueue.AddRange(CurrentWave.enemies);
            timer = CurrentWave.timer;
            StartWave();
        }
    }

    void Start()
    {
        StartWave();
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
        Debug.Log("Wave number: " + currentWaveIndex);
        started = true;
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        while (started && spawnQueue.Count > 0)
        {
            Collider2D spawnArea = StageManager.Instance.enemySpawnArea;
            Vector3 spawnPoint = new Vector3(
                UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                UnityEngine.Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                UnityEngine.Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

            if (CurrentWave.isRandom)
            {
                int randomIndex = UnityEngine.Random.Range(0, spawnQueue.Count);
                EnemyController enemy = Instantiate(spawnQueue[randomIndex]).GetComponent<EnemyController>();
                enemy.transform.position = spawnPoint;
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
                    EnemyController enemy = Instantiate(toSpawn).GetComponent<EnemyController>();
                    enemy.transform.position = spawnPoint;
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
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        currentEnemies.Remove(enemy);
        if (spawnQueue.Count == 0 && currentEnemies.Count == 0 && !CurrentWave.isLooping)
        {
            EndWave();
        }
    }

    public void SpawnEnemy(GameObject enemyObj, Vector2 spawnPosition)
    {
        EnemyController enemy = Instantiate(enemyObj).GetComponent<EnemyController>();
        enemy.transform.position = spawnPosition;
        Bounds landArea = StageManager.Instance.landingAreas[UnityEngine.Random.Range(0, StageManager.Instance.landingAreas.Count)].bounds;
        Vector3 landPoint = new Vector3(
                UnityEngine.Random.Range(landArea.min.x, landArea.max.x),
                UnityEngine.Random.Range(landArea.min.y, landArea.max.y),
                UnityEngine.Random.Range(landArea.min.z, landArea.max.z));
        enemy.Land(landPoint);
        currentEnemies.Add(enemy);
    }
}
