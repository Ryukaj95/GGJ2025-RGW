using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 distanceToPlayer = new Vector2(-1, -1);
    [SerializeField] private float speed = 1f;

    private Vector2 playerPosition => PlayerController.Instance.transform.position;
    private Vector2 endPosition => playerPosition + distanceToPlayer;

    private void Update() {
        Move();
    }

    private void Move() {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, endPosition, step);
    }
}
