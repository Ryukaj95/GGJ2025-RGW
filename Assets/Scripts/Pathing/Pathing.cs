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
        public Vector2 position;

        [SerializeField]
        public float stepTime;

        [SerializeField]
        public bool isAbsolute;

    }

    private bool canMove = true;

    private Step currentStep => steps[indexPosition];

    public Vector2 GetStartingPosition()
    {
        return steps.Length > 0 ? steps[0].position : new Vector2(0, 0);
    }

    public Vector2 GetNextPosition()
    {
        if (currentStep.stepTime > 0)
        {
            StartCoroutine(WaitForStepPause(currentStep.stepTime));
        }

        indexPosition = (indexPosition + 1) % steps.Length;

        Vector2 startPos = this.transform.position;

        return currentStep.isAbsolute ? currentStep.position : startPos + currentStep.position;
    }

    public Vector2 GetCurrentPosition()
    {
        return currentStep.position;
    }

    public bool CanMove()
    {
        return canMove;
    }

    public void SetMove(bool move)
    {
        canMove = move;
    }

    private IEnumerator WaitForStepPause(float stepTime)
    {
        canMove = false;
        yield return new WaitForSeconds(stepTime);
        canMove = true;
    }
}
