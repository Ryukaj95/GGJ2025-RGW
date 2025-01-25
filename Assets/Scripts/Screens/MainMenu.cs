using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float waitTimeBeforeStart = 2f;

    private bool isStarting = false;

    private void Update()
    {
        if (Input.anyKeyDown && !isStarting)
        {

            StartCoroutine(StartRoutine());
        }
    }

    private IEnumerator StartRoutine()
    {
        isStarting = true;

        yield return new WaitForSeconds(waitTimeBeforeStart);
        ScenesManager.Instance.StartGame();

        isStarting = false;
    }
}
