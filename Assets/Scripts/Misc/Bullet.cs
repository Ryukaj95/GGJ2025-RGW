using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private bool isFriendlyToPlayer = true;
    [SerializeField] private float bulletSpeed = 4f;

    [System.Serializable]
    private class Boost {
        [SerializeField] private bool doesBoost = false;
        public bool DoesBoost => doesBoost;

        [SerializeField] private float bulletSpeedAfterBoost = 4f;
        public float BulletSpeedAfterBoost => doesBoost ? bulletSpeedAfterBoost : 0f;

        [SerializeField] private float speedAfterSeconds = 0;
        public float SpeedAfterSeconds => doesBoost ? speedAfterSeconds : 0f;
    }

    [SerializeField] private Boost boostSettings;


    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        if (boostSettings.DoesBoost) {
            StartCoroutine(ChangeSpeedAfterSeconds(boostSettings.BulletSpeedAfterBoost, boostSettings.SpeedAfterSeconds));
        }
    }

    private void Update() {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isFriendlyToPlayer && other.gameObject.GetComponent<Enemy>()) {
            Destroy(this.gameObject);
            return;
        }

        if (!isFriendlyToPlayer && other.gameObject.GetComponent<PlayerController>()) {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Move() {
        rb.MovePosition(rb.position + movementDirection * (bulletSpeed * Time.fixedDeltaTime));
    }

    private IEnumerator ChangeSpeedAfterSeconds(float speed, float seconds) {
        yield return new WaitForSeconds(seconds);
        bulletSpeed = speed;
    }

    public void SetTarget(Vector2 target) {
        movementDirection = (target - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;
    }

    public void SetDirection(Vector2 direction) {
        movementDirection = direction.normalized;
    }

    public void SetIsFriendlyToPlayer(bool value) {
        isFriendlyToPlayer = value;
    }

    public void SetSpeed(float speed) {
        bulletSpeed = speed;
    }
}
