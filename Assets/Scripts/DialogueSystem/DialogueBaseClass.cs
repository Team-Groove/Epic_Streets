using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueBaseClass : MonoBehaviour
{
    public bool finished { get; protected set; }


    protected IEnumerator WriteText(string input, TextMeshProUGUI textHolder, float delay)
   {
        for (int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);
        
        finished = true;
    }

}
