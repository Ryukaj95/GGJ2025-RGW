using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Credits : Singleton<Credits>
{
    [SerializeField] private float scrollSpeed = 12f;
    [SerializeField] private GameObject scrollableContent;

    private Rigidbody2D scrollableCredits;
    private bool isScrolling = true;

    protected override void Awake()
    {
        base.Awake();

        scrollableCredits = scrollableContent.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (isScrolling) {
            scrollableCredits.transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
        }
    }

    public void StopScrolling() {
        isScrolling = false;
    }
}
