using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    
    [SerializeField] private GameObject dialogue;

    [SerializeField] private GameObject EToTalk;

    public bool canTalkTo;

    private void Start()
    {
        EToTalk.SetActive(false);
    }

    private void Update()
    {
        if (dialogue.activeSelf) canTalkTo = false;
        else canTalkTo = true;
    }

    public bool DialogueActive()
    {
        return dialogue.activeSelf;
    }

    public void ActivateDialogue()
    {
        dialogue.SetActive(true);
    }
    public bool CanTalk()
    {
        return canTalkTo;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EToTalk.gameObject.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EToTalk.gameObject.SetActive(false);
        }
    }

}
