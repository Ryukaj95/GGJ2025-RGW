using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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

    private void Awake()
    {
        enemyPathing = GetComponent<Pathing>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        startingPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        endPosition = startingPosition + enemyPathing.GetCurrentPosition();
    }

    private void Start()
    {
        enemySprites.Add(spriteRenderer.sprite);
        enemySprites.Add(Resources.Load<Sprite>("Sprites/explosion1"));
        enemySprites.Add(Resources.Load<Sprite>("Sprites/explosion2"));
    }

    private void Update()
    {
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
        this.transform.position = Vector2.MoveTowards(this.transform.position, endPosition, step);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Arena>())
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
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
        enemyPathing.SetMove(false);
        if (enemySprites.Count == 0)
        {
            Destroy(this.gameObject);
            yield break;
        }
        do
        {
            spriteIndex = (spriteIndex + 1) % enemySprites.Count;
            spriteRenderer.sprite = enemySprites[spriteIndex];
            yield return new WaitForSeconds(0.3f);
        } while (spriteIndex > 0);
        StageManager.Instance.DropPoints(this.transform.position);
        Destroy(this.gameObject);
    }

    public IEnumerator DamageAnimation()
    {
        spriteRenderer.color = damage;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = transparent;
    }

}
