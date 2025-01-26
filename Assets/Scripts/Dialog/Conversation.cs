using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "MBS/Dialogue", order = 1)]
public class DialogueScriptableObj : ScriptableObject
{
    public Dialogue[] dialogs;
}
