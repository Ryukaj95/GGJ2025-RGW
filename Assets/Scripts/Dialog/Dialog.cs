using UnityEngine;

[System.Serializable]
    public class Dialogue {
        public string characterName;
        public string dialogueText;
        public int position;

        public Dialogue(string cN, string d, int p) {
            this.characterName = cN;
            this.dialogueText = d;
            this.position = p;
        }
}