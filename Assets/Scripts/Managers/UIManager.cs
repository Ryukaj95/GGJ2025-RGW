using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    protected override void Awake() {
        base.Awake();

        // StartCoroutine(TestScoreRoutine());
    }

    public void ResetScore() {
        score = 0;
        scoreText.text = score.ToString();
    }
    
    public void AddScore(int value) {
        score += value;
        scoreText.text = score.ToString();
    }

    private IEnumerator TestScoreRoutine() {
        Debug.Log("TestScoreRoutine started!");
        yield return new WaitForSeconds(2f);

        Debug.Log("Adding 10 points...");
        AddScore(10);

        yield return new WaitForSeconds(2f);

        Debug.Log("Adding 20 points...");
        AddScore(20);
    }
}
