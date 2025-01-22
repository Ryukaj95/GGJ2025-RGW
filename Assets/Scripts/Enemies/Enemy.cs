using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 endPosition;
    [SerializeField] private float speed = 1f;

    private Pathing enemyPathing;

    private void Awake()
    {
        enemyPathing = GetComponent<Pathing>();
    }

    private void Update()
    {
        if (endPosition != null)
        {
            if (this.transform.position.Equals(endPosition))
            {
                endPosition = enemyPathing.GetNextPosition();
            }

            UpdateMovement();
        }
    }

    private void UpdateMovement()
    {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, endPosition, step);
    }
}
