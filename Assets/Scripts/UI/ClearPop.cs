using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ClearPop : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;

    private float progress = 0f;

    private void Start() {
        // StartCoroutine(TestPopLoad());
        // StartCoroutine(TestPopDeplete());
    }

    public void SetProgress(float _progress) {
        progress = Math.Clamp(_progress, 0f, 1f);
        progressBar.SetProgress(progress);
    }

    public void AddProgress(float _progress) {
        progress = Math.Clamp(progress + _progress, 0f, 1f);
        progressBar.SetProgress(progress);
    }

    public void RemoveProgress(float _progress) {
        progress = Math.Clamp(progress - _progress, 0f, 1f);
        progressBar.SetProgress(progress);
    }

    public float GetProgress() {
        return progress;
    }

    private IEnumerator TestPopLoad() {
        Debug.Log("TestPopLoad: Start");

        while (progress <= 1f) {
            Debug.Log("progress: " + progress);
            
            progress = Math.Clamp(progress + 0.1f, 0f, 1f);
            progressBar.SetProgress(progress);

            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator TestPopDeplete() {
        Debug.Log("TestPopDeplete: Start");

        yield return new WaitUntil(() => progress >= 0.7f);
        progress -= 0.33f;
        progressBar.SetProgress(progress);
    }
}
