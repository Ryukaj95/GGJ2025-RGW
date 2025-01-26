using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameScore : Singleton<EndGameScore>
{
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI livesLeftText;
    [SerializeField] private TextMeshProUGUI grazeText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI raingText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void OnEnable()
    {
        ClearAll();

        //StartCoroutine(TestEndGameScoreRoutine());
    }

    public void SetKills(int value)
    {
        killsText.text = value.ToString();
    }

    public void SetLivesLeft(int value)
    {
        livesLeftText.text = value.ToString();
    }

    public void SetGraze(int value)
    {
        grazeText.text = value.ToString();
    }

    // Time is in milliseconds
    public void SetTime(int value)
    {
        System.TimeSpan timeSpan = System.TimeSpan.FromMilliseconds(value);
        timeText.text = timeSpan.ToString(@"mm\:ss\.ff");
    }

    public void SetRating(string value)
    {
        raingText.text = value;
    }

    public void SetFinalScore(int value)
    {
        finalScoreText.text = value.ToString("D9");
    }

    public void SetAll(int kills, int livesLeft, int graze, int time, string rating, int finalScore)
    {
        SetKills(kills);
        SetLivesLeft(livesLeft);
        SetGraze(graze);
        SetTime(time);
        SetRating(rating);
        SetFinalScore(finalScore);
    }

    public void ClearAll()
    {
        killsText.text = "";
        livesLeftText.text = "";
        grazeText.text = "";
        timeText.text = "";
        raingText.text = "";
        finalScoreText.text = "";
    }

    private IEnumerator TestEndGameScoreRoutine()
    {
        yield return new WaitForSeconds(1f);

        SetAll(10, 3, 20, 1345600, "A", 16000);
    }
}
