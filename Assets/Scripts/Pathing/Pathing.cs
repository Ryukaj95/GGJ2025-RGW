using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Pathing : MonoBehaviour
{

    [SerializeField]
    Step[] positions = new Step[] { };

    [SerializeField]
    private int indexPosition = 0;

    [System.Serializable]
    private class Step
    {
        [SerializeField]
        public Vector2 position;

        [SerializeField]
        public int stepTime;
    }

    private bool canMove = true;

    private Step currentStep => positions[indexPosition];

    public Vector2 GetNextPosition()
    {
        if (currentStep.stepTime > 0) {
            StartCoroutine(WaitForStepPause(currentStep.stepTime));
        }

        indexPosition = (indexPosition + 1) % positions.Length;
        return currentStep.position;
    }
    
    public Vector2 GetCurrentPosition() {
        return currentStep.position;
    }

    public bool CanMove() {
        return canMove;
    }

    private IEnumerator WaitForStepPause(int stepTime) {
        canMove = false;
        yield return new WaitForSeconds(stepTime);
        canMove = true;
    }
}
