using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private GameObject positionImageOne;
    [SerializeField] private GameObject positionImageTwo;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject namePanel;

    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;

    [SerializeField] private float textSpeed = 0.3f;
    [SerializeField] private float textFinishWaitTime = 1f;

    private Color emptyColor = new Color(1, 1, 1, 0);
    private Color visibleColor = new Color(1, 1, 1, 1);
    private Color backgroundColor = new Color(0.6f, 0.6f, 0.6f, 1);

    private bool isSpeaking = false;

    private class Dialogue {
        public string imageResourcePath;
        public string characterName;
        public string dialogueText;
        public int position;

        public Dialogue(string cN, string d, int p, string iRP) {
            this.characterName = cN;
            this.dialogueText = d;
            this.position = p;
            this.imageResourcePath = iRP;
        }
    }

    private List<Dialogue> dialogues = new List<Dialogue>();

    protected override void Awake() {
        base.Awake();

        // StartCoroutine(TestAddDialoguesAfterSecRoutine());
    }

    private void Update() {
        if (!isSpeaking && dialogues.Count > 0) {
            StartCoroutine(StartDialogueRoutine());
        }
    }

    public void ShowImage(Texture2D sourceImage, int position) {
        Image imageInScene = GetImage(position);

        Debug.Log(imageInScene);

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

    public void SetSpeakingImg(int position) {
        SetImageToFront(position);
        SetImageToBackground(position == 1 ? 2 : 1);
    }

    public void AddDialogue(string character, string text, int position) {
        dialogues.Add(new Dialogue(character, text, position, "Characters/" + character));
    }

    public void AddDialogue(string character, string text, int position, string charImageSourcePath) {
        dialogues.Add(new Dialogue(character, text, position, charImageSourcePath));
    }

    private void SetSpeakingName(string charName) {
        nameText.text = charName;
    }

    private void ClearDialogue() {
        dialogueText.text = "";
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

    private IEnumerator SpeakRoutine(string text) {
        yield return null;
        foreach (char letter in text) {
            dialogueText.text = dialogueText.text + letter;
            yield return new WaitForSeconds(textSpeed / 10);
        }
    }

    private IEnumerator StartDialogueRoutine() {
        isSpeaking = true;

        while (dialogues.Count > 0) {
            Dialogue currDialog = dialogues.First();
            dialogues.RemoveAt(0);

            ShowImage(Resources.Load<Texture2D>(currDialog.imageResourcePath), currDialog.position);
            yield return StartCoroutine(SayDialogueRoutine(currDialog));
        }

        isSpeaking = false;
    }

    private IEnumerator SayDialogueRoutine(Dialogue dialogue) {
        ClearDialogue();

        SetSpeakingImg(dialogue.position);
        SetSpeakingName(dialogue.characterName);
        
        yield return StartCoroutine(SpeakRoutine(dialogue.dialogueText));
        yield return new WaitForSeconds(textFinishWaitTime);
    }

    private IEnumerator TestAddDialoguesAfterSecRoutine() {
        yield return new WaitForSeconds(0.5f);

        AddDialogue("Shad", "You can't save her!", 2);
        AddDialogue("Dash", "I will stop you, Shad! I will stop you, Shad! I will stop you, Shad! I will stop you, Shad! I will stop you, Shad! I will stop you, Shad! I will stop you, Shad!", 1);
        AddDialogue("Shad", "No, you won't. MWUAHAHAHAHAHAHAHAHAH", 2);
        AddDialogue("Dash", "... stupid", 1);

        yield return new WaitForSeconds(0.5f);
        AddDialogue("Colonel", "I will help you", 2);
    }
}
