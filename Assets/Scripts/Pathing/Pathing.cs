using System.Collections;
using UnityEngine;

public class Pathing : MonoBehaviour
{

    [SerializeField]
    Step[] steps = new Step[] { };

    [SerializeField]
    private int indexPosition = 0;

    [System.Serializable]
    private class Step
    {
        [SerializeField]
        public Vector2 relativeVector;

        [SerializeField]
        public int stepTime;

        [SerializeField]
        public float arcHeight;

    }

    private bool canMove = true;

    private float progress = 0f;

    private Step currentStep => steps[indexPosition];

    public Vector2 GetStartingPosition()
    {
        return steps.Length > 0 ? steps[0].relativeVector : new Vector2(0, 0);
    }

    public Vector2 GetNextPosition()
    {
        if (currentStep.stepTime > 0)
        {
            StartCoroutine(WaitForStepPause(currentStep.stepTime));
        }

        indexPosition = (indexPosition + 1) % steps.Length;

        return currentStep.relativeVector;
    }

    public void advanceStep(float add, Vector2 endPosition)
    {
        progress += add / Vector2.Distance(transform.position, endPosition);
    }

    public void newStep()
    {
        progress = 0f;
    }

    public Vector2 GetCurrentPosition()
    {
        return currentStep.relativeVector;
    }

    public bool CanMove()
    {
        return canMove;
    }

    private IEnumerator WaitForStepPause(int stepTime)
    {
        canMove = false;
        yield return new WaitForSeconds(stepTime);
        canMove = true;
    }
}
