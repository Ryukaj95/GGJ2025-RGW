using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : Singleton<CutsceneManager>
{
    private DialogueManager dialogueManager;

    protected override void Awake() {
        base.Awake();

        dialogueManager = DialogueManager.Instance;

        StartCoroutine(TestSetupDialogue());
    }

    public void StartConversation() {
        dialogueManager.Show();
    }

    public void CloseConversation() {
        dialogueManager.Hide();
    }

    public void LoadConversation(DialogueScriptableObj conversation) {
        // Load conversation
        Debug.Log(conversation);
        foreach (Dialogue dialogue in conversation.dialogs) {
            dialogueManager.Add(dialogue);
        }
    }

    private IEnumerator TestSetupDialogue() {
        yield return new WaitForSeconds(2f);

        StartConversation();

        LoadConversation(
            Resources.Load<DialogueScriptableObj>("Conversations/Conversation1")
        );

        yield return DialogueManager.Instance.WaitForDialogueToFinish();

        CloseConversation();
    }
}
