using System.Collections;
using UnityEngine;

public class CutsceneManager : Singleton<CutsceneManager>
{
    protected override void Awake() {
        base.Awake();

        // StartCoroutine(TestSetupDialogue());
    }

    public void StartConversation() {
        DialogueManager.Instance.Show();
    }

    public void CloseConversation() {
        DialogueManager.Instance.Hide();
    }

    public void LoadConversation(DialogueScriptableObj conversation) {
        // Load conversation
        foreach (Dialogue dialogue in conversation.dialogs) {
            DialogueManager.Instance.Add(dialogue);
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
