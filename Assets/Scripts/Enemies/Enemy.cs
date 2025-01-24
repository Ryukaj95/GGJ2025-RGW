using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 endPosition;
    [SerializeField] private float speed = 1f;

    [SerializeField] public int health = 1;

    private Vector2 startingPosition;

    private Pathing enemyPathing;



    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] playerSprites;
    [SerializeField] private int spriteIndex = 0;

    private void Awake()
    {
        enemyPathing = GetComponent<Pathing>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startingPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        endPosition = startingPosition + enemyPathing.GetCurrentPosition();
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
            StartCoroutine(NextSprite());
        }
    }

    public IEnumerator NextSprite()
    {
        enemyPathing.SetMove(false);
        do
        {
            spriteIndex = (spriteIndex + 1) % playerSprites.Length;
            spriteRenderer.sprite = playerSprites[spriteIndex];
            yield return new WaitForSeconds(0.3f);
        } while (spriteIndex > 0);
        StageManager.Instance.DropPoints(this.transform.position);
        Destroy(this.gameObject);
    }

}
