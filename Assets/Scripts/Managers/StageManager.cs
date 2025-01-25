using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{

    [Header("Areas")]
    [SerializeField] public bool isPaused = false;

    [SerializeField] public Collider2D playerArena;

    [SerializeField] public Collider2D bulletArena;

    [SerializeField] public Collider2D gameArena;

    [SerializeField] public List<Collider2D> spawnAreas = new List<Collider2D>();

    [SerializeField] public List<Collider2D> landingAreas = new List<Collider2D>();

    public Collider2D enemySpawnArea => spawnAreas[UnityEngine.Random.Range(0, spawnAreas.Count)];

    [Header("Player")]
    [SerializeField] public Points points;

    [SerializeField] public int stageLevel;

    [SerializeField] public bool stopShooting = false;

    [Header("RATING")]
    [SerializeField] public float time = 300;
    [SerializeField] public int hitChain = 0;
    [SerializeField] public int score = 0;
    [SerializeField] public int graze = 0;
    [SerializeField] public int kills = 0;

    public void Update()
    {
        if (!isPaused)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                EndStage();
            }
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);

            //Debug.Log(minutes + ":" + seconds); // Da mostrare a schermo
        }
    }

    public void EndStage()
    {
        // Logic here
    }

    public void Lose()
    {

    }

    public void DropPoints(UnityEngine.Vector2 pos)
    {
        Instantiate(points, pos, UnityEngine.Quaternion.identity);
    }

    public void AddPoints(int points)
    {
        score += points;
    }

}
