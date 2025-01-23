using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private GameObject positionImageOne;
    [SerializeField] private GameObject positionImageTwo;

    private Color emptyColor = new Color(1, 1, 1, 0);
    private Color visibleColor = new Color(1, 1, 1, 1);
    private Color backgroundColor = new Color(0.6f, 0.6f, 0.6f, 1);

    public void ShowImage(Texture2D sourceImage, int position) {
        Image imageInScene = GetImage(position);

        if (!imageInScene) return;
        
        Sprite sprite = Sprite.Create(
            sourceImage,
            new Rect(0, 0, sourceImage.width, sourceImage.height),
            new Vector2(0.5f, 0.5f)
        );

        imageInScene.sprite = sprite;
        imageInScene.color = visibleColor;
    }

    public void RemoveImage(int position) {
        Image imageInScene = GetImage(position);

        if (!imageInScene) return;

        imageInScene.sprite = null;
        imageInScene.color = emptyColor;
    }

    private void SetImageToBackground(int position) {
        Image imageInScene = GetImage(position);

        if (!imageInScene || imageInScene.sprite == null) return;

        imageInScene.color = backgroundColor;
    }

    private void SetImageToFront(int position) {
        Image imageInScene = GetImage(position);

        if (!imageInScene || imageInScene.sprite == null) return;

        imageInScene.color = visibleColor;
    }

    public void Speak(int position) {
        SetImageToFront(position);
        SetImageToBackground(position == 1 ? 2 : 1);
    }

    private Image GetImage(int position) {
        if (position != 1 && position != 2) {
            return null;
        }

        if (position == 1) {
            return positionImageOne.GetComponent<Image>();
        } else {
            return positionImageTwo.GetComponent<Image>();
        }
    }

    protected override void Awake() {
        base.Awake();

        StartCoroutine(SetFirstImageRoutine());
    }

    private IEnumerator SetFirstImageRoutine() {
        ShowImage((Texture2D)Resources.Load("Characters/Dash"), 1);
        ShowImage((Texture2D)Resources.Load("Characters/Colonel"), 2);
        Speak(1);

        yield return new WaitForSeconds(1);
        Speak(2);

        yield return new WaitForSeconds(1);
        RemoveImage(2);

        yield return new WaitForSeconds(1);
        ShowImage((Texture2D)Resources.Load("Characters/Shad"), 2);
        Speak(2);

        yield return new WaitForSeconds(1);
        Speak(1);

        yield return new WaitForSeconds(0.5f);
        Speak(2);

        yield return new WaitForSeconds(3);
        Speak(1);
        
        yield return new WaitForSeconds(1);
        RemoveImage(2);
    }
}
