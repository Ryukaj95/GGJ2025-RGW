using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ClearPop clearPop;
    [SerializeField] private Funko funko;
    [SerializeField] private LightsHandler lightsHandler;
    [SerializeField] private GreetMessage greetMessage;
    [SerializeField] private ChainScore chainScore;

    private int score = 0;

    private string scoreStr => score.ToString("D12");

    protected override void Awake()
    {
        base.Awake();

        // StartTestsUI();
    }

    private void StartTestsUI()
    {
        // StartCoroutine(TestScoreRoutine());
        // StartCoroutine(TestFunkoRoutine());
        // StartCoroutine(TestPopRoutine());
        // StartCoroutine(TestLedRoutine());
        // StartCoroutine(TestGreetMessageRoutine());
        // StartCoroutine(TestChainScoreRoutine());
    }

    // SCORE RELATED METHODS

    public void ResetScore()
    {
        score = 0;
        scoreText.text = scoreStr;
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = scoreStr;
    }

    public void SetScore(int value)
    {
        score = value;
        scoreText.text = scoreStr;
    }

    public int GetScore()
    {
        return score;
    }

    // POP RELATED METHODS

    public void SetPopProgress(float value)
    {
        clearPop.SetProgress(value);
    }

    public void AddPopProgress(float value)
    {
        clearPop.AddProgress(value);
    }

    public void RemovePopProgress(float value)
    {
        clearPop.RemoveProgress(value);
    }

    public float GetCurrentPopProgress()
    {
        return clearPop.GetProgress();
    }

    // FUNKO RELATED METHODS

    public void SetFunko(int _value)
    {
        funko.SetFunko(Math.Clamp(_value, 0, 5));
    }

    public void NextFunko()
    {
        funko.NextFunko();
    }

    public int GetCurrentFunko()
    {
        return funko.GetCurrentFunko();
    }

    public void ResetFunko()
    {
        funko.ResetFunko();
    }

    // LIGHTS RELATED METHODS
    // ALL LIGHTS

    public void TurnOffLights()
    {
        lightsHandler.TurnOffAllLights();
    }

    public void TurnOnLights()
    {
        lightsHandler.TurnOnAllLights();
    }

    // MISSION FAILED LIGHT

    public void ToggleMissionFailedLight()
    {
        lightsHandler.ToggleMissionFailedLight();
    }

    public void TurnOnLeavingLight()
    {
        lightsHandler.TurnOnMissionFailedLight();
    }

    public void TurnOffLeavingLight()
    {
        lightsHandler.TurnOffMissionFailedLight();
    }

    // RELOADING LIGHT

    public void ToggleReloadingLight()
    {
        lightsHandler.ToggleReloadingLight();
    }

    public void TurnOnReloadingLight()
    {
        lightsHandler.TurnOnReloadingLight();
    }

    public void TurnOffReloadingLight()
    {
        lightsHandler.TurnOffReloadingLight();
    }

    // HIT LIGHT

    public void ToggleHitLight()
    {
        lightsHandler.ToggleHitLight();
    }

    public void TurnOnHitLight()
    {
        lightsHandler.TurnOnHitLight();
    }

    public void TurnOffHitLight()
    {
        lightsHandler.TurnOffHitLight();
    }

    // CRITICAL LIGHT

    public void ToggleCriticalLight()
    {
        lightsHandler.ToggleCriticalLight();
    }

    public void TurnOnCriticalLight()
    {
        lightsHandler.TurnOnCriticalLight();
    }

    public void TurnOffCriticalLight()
    {
        lightsHandler.TurnOffCriticalLight();
    }

    // DANGER LIGHT

    public void ToggleDangerLight()
    {
        lightsHandler.ToggleDangerLight();
    }

    public void TurnOnDangerLight()
    {
        lightsHandler.TurnOnDangerLight();
    }

    public void TurnOffDangerLight()
    {
        lightsHandler.TurnOffDangerLight();
    }

    // GREET MESSAGE RELATED METHODS

    public IEnumerator Speak(string text)
    {
        return greetMessage.Speak(text);
    }

    public void ClearGreetMessage()
    {
        greetMessage.ClearGreetMessage();
    }

    // CHAIN SCORE RELATED METHODS

    public void ResetChainScore()
    {
        chainScore.ResetChainScore();
    }

    public void AddChainScore(int value)
    {
        chainScore.AddChainScore(value);
    }

    public void SetChainScore(int value)
    {
        chainScore.SetChainScore(value);
    }

    public int GetChainScore()
    {
        return chainScore.GetChainScore();
    }

    // TEST ROUTINES

    private IEnumerator TestScoreRoutine()
    {
        Debug.Log("TestScoreRoutine started!");
        yield return new WaitForSeconds(2f);

        while (true)
        {
            yield return new WaitForSeconds(1f);
            int score = UnityEngine.Random.Range(100, 1001);
            Debug.Log("Score: " + score);
            AddScore(score);
        }
    }

    private IEnumerator TestFunkoRoutine()
    {
        Debug.Log("TestFunkoRoutine started!");
        yield return new WaitForSeconds(2f);

        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (funko.GetCurrentFunko() == 5)
            {
                funko.ResetFunko();
            }
            else
            {
                funko.NextFunko();
            }
        }
    }

    private IEnumerator TestPopRoutine()
    {
        Debug.Log("TestPopRoutine started!");
        yield return new WaitForSeconds(2f);

        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (clearPop.GetProgress() == 1f)
            {
                RemovePopProgress(1f);
            }
            else
            {
                float progress = UnityEngine.Random.Range(0.01f, 0.05f);
                Debug.Log("Progress: " + progress);
                AddPopProgress(progress);
            }
        }
    }

    private IEnumerator TestLedRoutine()
    {
        Debug.Log("TestLedRoutine started!");

        while (true)
        {
            yield return new WaitForSeconds(1f);

            lightsHandler.ToggleMissionFailedLight();
            lightsHandler.ToggleReloadingLight();
            lightsHandler.ToggleHitLight();
            lightsHandler.ToggleCriticalLight();
            lightsHandler.ToggleDangerLight();
        }
    }

    private IEnumerator TestGreetMessageRoutine()
    {
        Debug.Log("TestGreetMessageRoutine started!");

        while (true)
        {
            yield return new WaitForSeconds(2f);

            yield return Speak("Hello, World!");
            yield return new WaitForSeconds(1f);

            yield return Speak("How are you?");
            yield return new WaitForSeconds(1f);

            yield return Speak("I'm fine, thank you!");
            yield return new WaitForSeconds(1f);

            yield return Speak("A very long message to test the multiline in the box!");
            yield return new WaitForSeconds(1f);
        }
    }

    // private IEnumerator TestChainScoreRoutine()
    // {
    //     Debug.Log("TestChainScoreRoutine started!");
    //     yield return new WaitForSeconds(2f);

    //     while (true)
    //     {
    //         yield return new WaitForSeconds(1f);

    //         AddChainScore(10);
    //         yield return new WaitForSeconds(1f);

    //         AddChainScore(20);
    //         yield return new WaitForSeconds(1f);

    //         AddChainScore(30);
    //         yield return new WaitForSeconds(1f);

    //         SetChainScore(100);
    //         yield return new WaitForSeconds(1f);

    //         ResetChainScore();
    //     }
    // }
}
