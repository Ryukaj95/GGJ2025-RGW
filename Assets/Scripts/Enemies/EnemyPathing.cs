using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] Vector2[] positions = new Vector2[] {
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(1, 1),
        new Vector2(0, 1),
    };

    private int indexPosition = 0;

    private Vector2 currentTargetPosition => positions[indexPosition];

    public Vector2 GetNextPosition() {
        indexPosition = (indexPosition + 1) % positions.Length;
        return currentTargetPosition;
    }
}
