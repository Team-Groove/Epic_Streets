using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueHolder : MonoBehaviour
{
    private IEnumerator dialogeSeq;
    private bool dialogueFinished;

    private bool canToAgain;

    private void OnEnable()
    {
        canToAgain = false;
        dialogeSeq = DialogueSequence();
        StartCoroutine(dialogeSeq);
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Deactivate();
            gameObject.SetActive(false);
            StopCoroutine(dialogeSeq);
        }
    }
   
    private IEnumerator DialogueSequence()
    {
        if (!dialogueFinished)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
        }
        else
        {
            int index = transform.childCount - 1;
            Deactivate();
            transform.GetChild(index).gameObject.SetActive(true);
            yield return new WaitUntil(() => transform.GetChild(index).GetComponent<DialogueLine>().finished);
        }

        dialogueFinished = true;
        gameObject.SetActive(false);
    
    }

    private void Deactivate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
