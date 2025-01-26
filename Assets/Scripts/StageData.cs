using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "MBS/Stage")]
public class StageData : ScriptableObject
{
    public string stageName;

    public DialogueScriptableObj startDialogue;

    public List<WaveData> waves;

    public DialogueScriptableObj endDialogue;

    public int finalScore;

    public Sprite dialogueBG;

    public Sprite stageBG;
}
