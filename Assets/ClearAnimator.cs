using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ClearAnimator : Singleton<ClearAnimator>
{
    [SerializeField] public Sprite clearSprite;
    [SerializeField] public SpriteRenderer spriteRenderer;

    public IEnumerator ClearAnimation()
    {
        float flashDuration = 1.5f;
        float flashInterval = 0.25f;
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration)
        {
            spriteRenderer.sprite = clearSprite;
            yield return new WaitForSeconds(flashInterval);
            spriteRenderer.sprite = null;
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval * 2;
        }

        spriteRenderer.sprite = null;
    }
}
