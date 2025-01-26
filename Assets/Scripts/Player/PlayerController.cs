using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>, DeathAnimation, TakeDamage
{
    [SerializeField] private float startMoveSpeed = 4f;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isInvincible => spriteRenderer.sprite != playerSprites[0];

    [SerializeField] private Sprite[] playerSprites;
    [SerializeField] private int spriteIndex = 0;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private float moveSpeed;

    [SerializeField] public int health = 3;

    [SerializeField] private Weapon playerWeapon;
    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();

        moveSpeed = startMoveSpeed;

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void LateUpdate()
    {
        LimitPosition();
    }

    private void FixedUpdate()
    {
        // Debug.Log(isInvincible);
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        playerControls.Combat.Shoot.performed += _ => playerWeapon.playerTrigger = true;
        playerControls.Combat.Shoot.canceled += _ => playerWeapon.playerTrigger = false;
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void LimitPosition()
    {
        Bounds bounds = StageManager.Instance.playerArena.bounds;
        Vector2 maxPos = new Vector3(Math.Max(bounds.min.x, Math.Min(rb.position.x, bounds.max.x)), Math.Max(bounds.min.y, Math.Min(rb.position.y, bounds.max.y)));
        rb.position = maxPos;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            StartCoroutine(DeathAnimation());
            if (health <= 0)
            {
                StageManager.Instance.Lose();
                // LOSE
            }
        }
    }

    public IEnumerator DeathAnimation()
    {
        do
        {
            spriteIndex = (spriteIndex + 1) % playerSprites.Length;
            spriteRenderer.sprite = playerSprites[spriteIndex];
            this.transform.localScale = Vector3.one;
            if (spriteIndex == 0) this.transform.localScale = Vector3.one / 2;
            yield return new WaitForSeconds(0.3f);
        } while (spriteIndex > 0);
    }

}
