using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "ScriptableObjects/Conversation", order = 1)]
public class DialogueScriptableObj : ScriptableObject {
    public Dialogue[] dialogs;
}
