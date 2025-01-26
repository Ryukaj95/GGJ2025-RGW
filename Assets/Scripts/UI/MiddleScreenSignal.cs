using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleScreenSignal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<ThxForPlayingSignal>()) {
            Credits.Instance.StopScrolling();
        }
    }
}
