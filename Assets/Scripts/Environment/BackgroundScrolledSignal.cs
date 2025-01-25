using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolledSignal : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.GetComponent<BackgroundImage>() != null)
        {
            BackgroundManager.Instance.HandleScrolledBackground(gameObject);
        }
    }
}
