using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public string bulletId = ""; // UUID
    [SerializeField] private bool isFriendlyToPlayer = true;

    [SerializeField] private int damage = 1;
    [SerializeField] private float bulletSpeed = 4f;

    [System.Serializable]
    public class Boost
    {
        [SerializeField] private bool doesBoost = false;
        public bool DoesBoost => doesBoost;

        [SerializeField] private float bulletSpeedAfterBoost = 4f;
        public float BulletSpeedAfterBoost => doesBoost ? bulletSpeedAfterBoost : 0f;

        [SerializeField] private float accelaration = 0f; // When 0, it's instant

        public float accelerationSpeed => doesBoost ? accelaration : 0f;

        [SerializeField] private float countdown = 0;
        public float Countdown => doesBoost ? countdown : 0f;

        public void SetBoost(float speed, float accel, float wait)
        {
            doesBoost = true;
            bulletSpeedAfterBoost = speed;
            accelaration = accel;
            countdown = wait;
        }
    }

    [SerializeField] public Boost boostSettings;
    private bool decelerate = false;
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (boostSettings.DoesBoost)
        {
            StartCoroutine(ChangeSpeedAfterSeconds(boostSettings.BulletSpeedAfterBoost, boostSettings.Countdown));
        }
    }

    private void Update()
    {
        if (decelerate)
        {
            Debug.Log("ACCELERATION");
            if (boostSettings.accelerationSpeed > 0)
            {
                bulletSpeed = bulletSpeed + (boostSettings.accelerationSpeed * Time.fixedDeltaTime);
                if (bulletSpeed >= boostSettings.BulletSpeedAfterBoost)
                {
                    decelerate = false;
                    bulletSpeed = boostSettings.BulletSpeedAfterBoost;
                }
            }
            else
            {
                bulletSpeed = bulletSpeed - Math.Abs(boostSettings.accelerationSpeed * Time.fixedDeltaTime);
                if (bulletSpeed <= boostSettings.BulletSpeedAfterBoost)
                {
                    decelerate = false;
                    bulletSpeed = boostSettings.BulletSpeedAfterBoost;
                }
            }
        }
        Move();
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (isFriendlyToPlayer && other.gameObject.GetComponent<Enemy>())
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
            return;
        }

        if (!isFriendlyToPlayer && other.gameObject.GetComponent<PlayerController>())
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(this.gameObject);
            return;
        }
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movementDirection * (bulletSpeed * Time.fixedDeltaTime));
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;

        // Imposta la rotazione del proiettile
        rb.rotation = angle - 90;
    }

    private IEnumerator ChangeSpeedAfterSeconds(float speed, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (this.boostSettings.accelerationSpeed == 0)
        {
            bulletSpeed = speed;
        }
        else decelerate = true;
    }

    public void SetTarget(Vector2 target)
    {
        movementDirection = (target - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;
    }

    public void SetDirection(Vector2 direction)
    {
        movementDirection = direction.normalized;
    }

    public void SetIsFriendlyToPlayer(bool value)
    {
        isFriendlyToPlayer = value;
    }

    public void SetSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    public float GetSpeed()
    {
        return bulletSpeed;
    }

    public void OnDestroy()
    {
        if (!isFriendlyToPlayer)
        {
            BulletsManager.Instance.RemoveBulletFromList(bulletId);
        }
    }
}
