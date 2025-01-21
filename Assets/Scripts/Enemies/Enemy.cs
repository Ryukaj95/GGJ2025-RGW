using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 endPosition = new Vector2(0, 0);
    [SerializeField] private float speed = 1f;

    private EnemyPathing enemyPathing;

    private void Awake() {
        enemyPathing = GetComponent<EnemyPathing>();
    }

    private void Update() {
        if (this.transform.position.Equals(endPosition)) {
            endPosition = enemyPathing.GetNextPosition();
        }

        // UpdateMovement();
    }

    private void UpdateMovement() {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, endPosition, step);
    }
}
