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

    [Header("Stage")]
    [SerializeField] public Points points;

    [SerializeField] public bool stopShooting = false;

    [SerializeField] public List<StageData> stages;

    private int stageIndex = 0;

    public StageData CurrentStage => stages[stageIndex];

    [Header("RATING")]
    [SerializeField] public float globalTime = 0;
    [SerializeField] public int hitChain = 0;
    [SerializeField] public int score = 0;
    [SerializeField] public int graze = 0;
    [SerializeField] public int kills = 0;


    public void Start()
    {
        isPaused = true;
        stopShooting = true;
        StartCoroutine(StartStage());
    }


    public IEnumerator StartStage()
    {
        ResetScore();
        UIManager.Instance.TurnOffLights();
        if (CurrentStage.dialogueBG != null) DialogueManager.Instance.UpdateBackground(CurrentStage.dialogueBG);
        if (CurrentStage.stageBG != null) BackgroundManager.Instance.UpdateBackground(CurrentStage.stageBG);
        if (CurrentStage.startDialogue != null)
        {
            yield return CutsceneManager.Instance.JumpstartDialogue(CurrentStage.startDialogue);
        }
        stopShooting = false;
        isPaused = false;
        WaveManager.Instance.waves = CurrentStage.waves;
        WaveManager.Instance.StartWave();
    }

    public void Update()
    {
        UIManager.Instance.SetScore(score);
    }

    public void FixedUpdate()
    {
        if (!isPaused)
        {
            globalTime += Time.deltaTime;

        }
    }

    public IEnumerator EndStage()
    {
        UIManager.Instance.TurnOnLeavingLight();
        yield return new WaitForSeconds(2f);
        if (CurrentStage.endDialogue != null)
        {
            yield return CutsceneManager.Instance.JumpstartDialogue(CurrentStage.endDialogue);
        }
        stopShooting = true;
        if (stageIndex == stages.Count - 1)
        {
            Win();
        }
        else
        {
            stageIndex++;
            StartCoroutine(StartStage());
        }
    }

    public void Lose()
    {
        Debug.Log("LOOOOOOOOSE");
    }
    public void Win()
    {
        Debug.Log("WIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIN");
    }

    public void DropPoints(UnityEngine.Vector2 pos)
    {
        Instantiate(points, pos, UnityEngine.Quaternion.identity);
    }

    public void AddPoints(int points)
    {
        score += points;
        UIManager.Instance.SetScore(score);
    }

    public void AddKill() {
        kills++;
        UIManager.Instance.SetChainScore(kills);
    }

    public void ResetKills() {
        kills = 0;
        UIManager.Instance.SetChainScore(kills);
    }

    public void ResetScore()
    {
        score = 0;
        graze = 0;
        hitChain = 0;
        kills = 0;
        globalTime = 0;
    }

}
