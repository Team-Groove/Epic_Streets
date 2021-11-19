using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Fighter
{
    #region VARIABLES

    [SerializeField] private NPCcontroller npc;
    private Keyboard keyboard;
    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        keyboard = Keyboard.current;
    }

    private void Update()
    {
        UpdateHealthBar(currenthealth);
        IfCurrentLifeisZero();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npc = collision.gameObject.GetComponentInParent<NPCcontroller>();

            Debug.Log("en zona");

            if (keyboard.eKey.isPressed)
            {
                Debug.Log("E");
                npc.ActivateDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npc = null;
        }
    }

    #endregion

    #region FUNCTIONS

    public bool InDialogue()
    {
        if (npc != null)
        {
            return npc.DialogueActive();
        }
        else
        {
            return false;
        }
    }

    #endregion
}
