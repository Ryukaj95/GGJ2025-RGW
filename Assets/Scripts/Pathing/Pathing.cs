using UnityEngine;

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


    private Vector2 currentTargetPosition => positions[indexPosition].position;

    public Vector2 GetNextPosition()
    {
        indexPosition = (indexPosition + 1) % positions.Length;
        return currentTargetPosition;
    }
}
