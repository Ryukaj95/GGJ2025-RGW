using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float waitTimeBeforeStart = 4f;

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip menuBGM;

    [SerializeField] AudioClip startUpSound;

    private bool isStarting = false;


    private void Start()
    {
        audioSource.clip = menuBGM;
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying) audioSource.Play();
        if (Input.anyKeyDown && !isStarting)
        {

            StartCoroutine(StartRoutine());
        }
    }

    private IEnumerator StartRoutine()
    {
        isStarting = true;
        audioSource.PlayOneShot(startUpSound);
        yield return new WaitForSeconds(waitTimeBeforeStart);
        ScenesManager.Instance.StartGame();

        isStarting = false;
    }
}
