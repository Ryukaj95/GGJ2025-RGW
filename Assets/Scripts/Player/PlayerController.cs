using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>, DeathAnimation, TakeDamage
{
    [SerializeField] private float startMoveSpeed = 4f;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private SpriteRenderer popSpriteRender;

    private bool isInvincible = false;

    private Vector2 spawnPosition;

    [SerializeField] private Sprite[] playerSprites;
    [SerializeField] private int spriteIndex = 0;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private float moveSpeed;

    private bool damageAnim = false;

    [SerializeField] public int health = 3;

    [SerializeField] private Weapon playerWeapon;

    [SerializeField] public Collider2D grazeRange;

    [SerializeField] private Sprite[] popSprites;


    [Range(0, 100)]
    public float popCharge = 0;

    [Range(0, 3)]
    private int popUses = 0;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = startMoveSpeed;

    }

    private void OnEnable()
    {
        spawnPosition = this.transform.position;
        UIManager.Instance.SetPopProgress(0f);
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
        if (!damageAnim) Move();
        if (!StageManager.Instance.isPaused) UpdatePopProgress();
    }

    public void UpdatePopProgress()
    {
        popCharge = Math.Clamp(popCharge += Time.deltaTime * 5, 0f, 100f);
        if (popCharge == 100f)
        {
            popUses = Math.Clamp(popUses + 1, 0, 3);
        }
        float popProgressBar = Math.Clamp((popUses + popCharge / 100) / 3, 0f, 1f);
        UIManager.Instance.SetPopProgress(popProgressBar);
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        playerControls.Combat.Shoot.performed += _ => playerWeapon.playerTrigger = true;
        playerControls.Combat.Shoot.canceled += _ => playerWeapon.playerTrigger = false;
        playerControls.Combat.Pop.performed += _ => callPop();
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
            StageManager.Instance.ResetKills();
            health -= damage;
            StartCoroutine(DeathAnimation());
            if (health == 1) UIManager.Instance.TurnOnCriticalLight();
            else if (health == 2) UIManager.Instance.TurnOnDangerLight();
            else
            {
                UIManager.Instance.TurnOffCriticalLight();
                UIManager.Instance.TurnOffCriticalLight();
            }
            if (health <= 0)
            {
                StageManager.Instance.Lose();
            }
        }
    }

    public IEnumerator DeathAnimation()
    {
        UIManager.Instance.TurnOnHitLight();
        isInvincible = true;
        damageAnim = true;
        do
        {
            spriteIndex = (spriteIndex + 1) % playerSprites.Length;
            if (spriteIndex != 0)
            {
                spriteRenderer.sprite = playerSprites[spriteIndex];
                this.transform.localScale = Vector3.one;
            }
            else
            {
                spriteRenderer.sprite = null;
                this.transform.localScale = Vector3.one / 2;
            }
            yield return new WaitForSeconds(0.3f);
        } while (spriteIndex > 0);
        damageAnim = false;
        UIManager.Instance.TurnOffHitLight();
        BulletsManager.Instance.PopAllBullets();
        StartCoroutine(InvincibleRespawn());
    }

    public IEnumerator InvincibleRespawn()
    {
        if (spawnPosition != null) transform.position = spawnPosition;
        int i = 0;
        do
        {
            spriteRenderer.sprite = null;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.sprite = playerSprites[0];
            yield return new WaitForSeconds(0.2f);
            i++;
        } while (i < 6);
        isInvincible = false;
    }

    private void callPop()
    {
        if (popUses > 0)
        {
            popUses--;
            popCharge = 0;
            StartCoroutine(PopAnimation());
            BulletsManager.Instance.PopAllBullets();
        }
    }

    public IEnumerator PopAnimation()
    {
        isInvincible = true;
        //sprite arcobaleno
        popSpriteRender.sprite = popSprites[0];
        yield return new WaitForSeconds(0.5f);
        popSpriteRender.sprite = popSprites[1];
        yield return new WaitForSeconds(0.5f);
        popSpriteRender.sprite = null;
        isInvincible = false;
    }
}
