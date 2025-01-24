using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int pointValue = 50;
    public float size = 0.5f;

    public float speed = 1;

    // Update is called once per frame
    void Awake()
    {
        pointValue = Random.Range(50, 200);
        size = pointValue / pointValue;
        this.transform.localScale = new Vector3(size, size, size);
    }

    void Update()
    {
        this.transform.position = transform.position + Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            StageManager.Instance.AddPoints(pointValue);
            Destroy(gameObject);
        }
    }
}
