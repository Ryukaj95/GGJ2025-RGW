using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreetMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI greetMessageTxt;
    [SerializeField] private float textSpeed = 0.3f;

    private void Awake() {
        ClearGreetMessage();
    }

    public void ClearGreetMessage() {
        greetMessageTxt.text = "";
    }

    public IEnumerator Speak(string text)
    {
        return SpeakRoutine(text);
    }

    private IEnumerator SpeakRoutine(string text)
    {
        ClearGreetMessage();
        greetMessageTxt.pageToDisplay = 1;

        foreach (char letter in text)
        {
            greetMessageTxt.text += letter;
            greetMessageTxt.ForceMeshUpdate();

            /*

            // Check if the current page is full
            if (greetMessageTxt.pageToDisplay < greetMessageTxt.textInfo.pageCount)
            {
                // Wait for the user to click before proceeding to the next page
                isWaitingForContinue = true;
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                isWaitingForContinue = false;
                greetMessageTxt.pageToDisplay++;
            }
            */

            yield return new WaitForSeconds(textSpeed / 10);
        }

        // Final wait for user input to confirm the end of the dialogue
        /*
        isWaitingForContinue = true;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        isWaitingForContinue = false;
        */
    }
}
