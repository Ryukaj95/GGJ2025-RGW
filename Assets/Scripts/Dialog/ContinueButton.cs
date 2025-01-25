using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private float movingSpeed = 0.7f;
    [SerializeField] private float shift = 4;

    private RectTransform buttonRT;
    private Vector2 initialButtonPosition;
    private Coroutine moveButtonRoutine;

    private void Start() {
        buttonRT = continueButton.GetComponent<RectTransform>();
        initialButtonPosition = buttonRT.anchoredPosition;
    }

    private void Update() {
        if (DialogueManager.Instance.isWaitingForContinue && moveButtonRoutine == null) {
            StartMoving();
            return;
        }

        if (!DialogueManager.Instance.isWaitingForContinue && moveButtonRoutine != null) {
            StopMoving();
            return;
        }
    }

    private void StartMoving() {
        moveButtonRoutine = StartCoroutine(MoveButtonRoutine());
    }

    private void StopMoving() {
        StopCoroutine(moveButtonRoutine);
        moveButtonRoutine = null;
        buttonRT.anchoredPosition = initialButtonPosition;
    }

    private IEnumerator MoveButtonRoutine() {
        Vector2 defaultPosition = buttonRT.anchoredPosition;
        Vector2 movedPosition = defaultPosition;
        movedPosition.y -= shift;

        while(true) {
            Debug.Log("Moving...");
            buttonRT.anchoredPosition = movedPosition;
            yield return new WaitForSeconds(movingSpeed);
            buttonRT.anchoredPosition = defaultPosition;
            yield return new WaitForSeconds(movingSpeed);
        }

    }
}
