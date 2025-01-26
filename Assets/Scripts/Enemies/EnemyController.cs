using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector2 endPosition;
    [SerializeField] private float speed = 1f;

    [SerializeField] public int health = 1;

    private Vector2 startingPosition;

    private Pathing enemyPathing;

    private SpriteRenderer spriteRenderer;
    private List<Sprite> enemySprites = new List<Sprite>();
    private int spriteIndex = 0;

    private Color transparent = Color.white;

    private Color damage = Color.red;

    public bool flee = false;

    public bool landing = false;

    private Vector2 landingPos = new Vector2(0, 0);

    public Weapon weapon;

    private void Awake()
    {
        enemyPathing = GetComponent<Pathing>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weapon = GetComponent<Weapon>();
        startingPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        endPosition = startingPosition + enemyPathing.GetCurrentPosition();
    }

    public void Land(Vector2 pos)
    {
        landing = true;
        landingPos = pos;
        endPosition = pos;
    }

    private void Start()
    {
        enemySprites.Add(spriteRenderer.sprite);
        enemySprites.Add(Resources.Load<Sprite>("Sprites/explosion1"));
        enemySprites.Add(Resources.Load<Sprite>("Sprites/explosion2"));
    }

    private void Update()
    {
        if (landing)
        {
            if (StageManager.Instance.gameArena.bounds.Contains(this.transform.position))
            {
                Debug.Log("contains");
            }
            this.transform.position = Vector2.MoveTowards(this.transform.position, landingPos, speed * Time.deltaTime);
            if (this.transform.position.Equals(landingPos))
            {
                landing = false;
            }
        }

        if (this.transform.position.Equals(endPosition))
        {
            endPosition = enemyPathing.GetNextPosition();
        }

        if (enemyPathing.CanMove())
        {
            UpdateMovement();
        }
    }
    private void UpdateMovement()
    {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, flee ? Vector2.up * 1000 : endPosition, step);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Arena>())
        {
            DestroyEnemy();
            return;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
        {
            StartCoroutine(DeathAnimation());
        }
        else
        {
            StartCoroutine(DamageAnimation());
        }
    }

    public IEnumerator DeathAnimation()
    {
        StageManager.Instance.kills += 1;
        enemyPathing.SetMove(false);
        do
        {
            spriteIndex = (spriteIndex + 1) % enemySprites.Count;
            spriteRenderer.sprite = enemySprites[spriteIndex];
            if (spriteIndex == 0) spriteRenderer.sprite = null;
            else yield return new WaitForSeconds(0.3f);
        } while (spriteIndex > 0);
        StageManager.Instance.DropPoints(this.transform.position);
        DestroyEnemy();
    }

    public IEnumerator DamageAnimation()
    {
        spriteRenderer.color = damage;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = transparent;
    }

    public void DestroyEnemy()
    {
        try
        {
            WaveManager.Instance.RemoveEnemy(this);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            Destroy(this.gameObject);
        }
    }

}
