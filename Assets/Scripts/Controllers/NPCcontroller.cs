using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCcontroller : MonoBehaviour
{
    
    [SerializeField] private GameObject dialogue;

    [SerializeField] private GameObject EToTalk;

    private void Start()
    {
        EToTalk.SetActive(false);
    }



    public void ActivateDialogue()
    {
        dialogue.SetActive(true);
    }
    public bool DialogueActive()
    {
        return dialogue.activeSelf;
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
