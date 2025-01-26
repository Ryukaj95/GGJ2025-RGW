using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChainScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chainScoreText;

    private int chainScore = 0;
    private string chainScoreStr => chainScore.ToString("D3") + "!";

    private void Awake() {
        ResetChainScore();

        // StartCoroutine(TestChainScoreRoutine());
    }

    public void ResetChainScore() {
        chainScore = 0;
        chainScoreText.text = chainScoreStr;
    }

    public void AddChainScore(int value) {
        chainScore += value;
        chainScoreText.text = chainScoreStr;
    }

    public void SetChainScore(int value) {
        chainScore = value;
        chainScoreText.text = chainScoreStr;
    }

    public int GetChainScore() {
        return chainScore;
    }

    private IEnumerator TestChainScoreRoutine() {
        yield return new WaitForSeconds(1f);

        AddChainScore(10);

        yield return new WaitForSeconds(1f);

        AddChainScore(20);

        yield return new WaitForSeconds(1f);

        AddChainScore(30);

        yield return new WaitForSeconds(1f);

        SetChainScore(100);

        yield return new WaitForSeconds(1f);

        ResetChainScore();
    }
}
