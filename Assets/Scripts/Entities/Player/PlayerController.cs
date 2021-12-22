using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : Fighter
{
    #region VARIABLES

   // public static PlayerController instance;

    private NPCController npc;
    
    //AFTER LOAD REMAINS

    public UIController uiController;
    private PlayersHealthBar playerhealthBar;
    private SceneController sceneController;


    private Keyboard keyboard;

    //DATA SAVE

    private string playerHpPrefName = "PlayerHp";
    private string playerMaxHpPrefName = "PlayerMaxHp";

    private float newpluslife = 100;

    public bool maxHpUpgraded = false;

    #endregion

    #region UNITY_CALLS


    public override void Awake()
    { 
        LoadData();
        
        playerhealthBar = GetComponent<PlayersHealthBar>();
        sceneController = FindObjectOfType<SceneController>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponentInChildren<BoxCollider2D>();

        if (SceneManager.GetSceneByName("MainHub").IsValid())
        {
            IsDead = false;
            if (sceneController.endGameNum > 0)
            {
                maxhealth += newpluslife;
            }
            currenthealth = maxhealth;
        }
        
    }

    private void Start()
    {
        keyboard = Keyboard.current; 
    }

    private void Update()
    {
        healthBar = GameObject.Find("UIController").gameObject.transform.Find("HealthBar").GetComponent<HealthBar>();
        uiController = GameObject.Find("UIController").GetComponent<UIController>();
        playerhealthBar.FindHealthBarSprite();

        UpdateHealthBar(currenthealth);
       

        healthBar.SetMaxHealth(maxhealth);

        if (maxHpUpgraded)
        {
            currenthealth = maxhealth;
            maxHpUpgraded = false;
        }

        IfCurrentLifeisZero();

        if (npc == null) return;

        else if (keyboard.eKey.isPressed && npc.canTalkTo)
        {
            npc.ActivateDialogue();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npc = collision.gameObject.GetComponentInParent<NPCController>();
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
    public void PauseInput(InputAction.CallbackContext context)
    {
        if (context.performed && !uiController.gameInPause)
        {
            uiController.gameInPause = true;
        }
        else if (context.performed && uiController.gameInPause)
        {
            uiController.gameInPause = false;
        }
    }
    public void ComboMenuInput(InputAction.CallbackContext context)
    {
        if (context.performed && !uiController.tabMenuOn)
        {
            uiController.TabMenu.gameObject.SetActive(true);
            uiController.tabMenuOn = true;
        }
        else if (context.performed && uiController.tabMenuOn)
        {
            uiController.TabMenu.gameObject.SetActive(false);
            uiController.tabMenuOn = false;
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(playerHpPrefName, currenthealth);
        PlayerPrefs.SetFloat(playerMaxHpPrefName, maxhealth);
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(playerHpPrefName))
        {
            currenthealth = PlayerPrefs.GetFloat(playerHpPrefName, maxhealth);
        }
        else
        {
            currenthealth = maxhealth;
            PlayerPrefs.SetFloat(playerHpPrefName, maxhealth);
        }

        if (PlayerPrefs.HasKey(playerMaxHpPrefName))
        {
            maxhealth = PlayerPrefs.GetFloat(playerMaxHpPrefName, 100);
        }
        else
        {
            maxhealth = 100;
            PlayerPrefs.SetFloat(playerMaxHpPrefName, 100);
        }

    }


    #endregion
}
