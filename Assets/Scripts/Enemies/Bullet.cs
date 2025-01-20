using System.Collections;
using System.Numerics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 0.25f;

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


    Rigidbody2D rb;
    UnityEngine.Vector2 movementDirection;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        movementDirection = PlayerController.Instance.transform.position - this.transform.position;

        if (boostSettings.DoesBoost) {
            StartCoroutine(IncreaseSpeedAfterSeconds(boostSettings.BulletSpeedAfterBoost, boostSettings.SpeedAfterSeconds));
        }
    }

    private void Update() {
        Move();
    }

    private void Move() {
        rb.MovePosition(rb.position + movementDirection * (bulletSpeed * Time.fixedDeltaTime));
    }

    private IEnumerator IncreaseSpeedAfterSeconds(float speed, float seconds) {
        yield return new WaitForSeconds(seconds);
        bulletSpeed = speed;
    }
}
