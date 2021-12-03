using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : Fighter
{
    #region VARIABLES

    public static PlayerController instance;

    private NPCcontroller npc;
    private UIController uiController;
    private PlayersHealthBar playerhealthBar;
    private PlayerAnimation playerAnimation;

    private Vector3 SpawnPointOnEnterLevel;
    private Keyboard keyboard;

    #endregion

    #region UNITY_CALLS

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    public override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }

        playerhealthBar = GetComponent<PlayersHealthBar>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponentInChildren<BoxCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();

        DontDestroyOnLoad(gameObject);
    }

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

            if (keyboard.eKey.isPressed && !npc.DialogueActive())
            {
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

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            uiController.gameObject.SetActive(false);
            return;
        }
        else
        {
            uiController = GameObject.FindObjectOfType<UIController>();
            uiController.gameObject.SetActive(true);

            playerAnimation.damageManager = FindObjectOfType<AttackDamageManager>();

            playerhealthBar.FindHealthBarSprite();

            healthBar = GameObject.Find("UIController").gameObject.transform.Find("HealthBar").GetComponent<HealthBar>();
            SpawnPointOnEnterLevel = GameObject.Find("PlayerSpawnPoint").gameObject.transform.position;

            transform.position = SpawnPointOnEnterLevel;
        }

        if (SceneManager.GetSceneByName("MainHub").isLoaded)
        {

            IsDead = false;
            
            uiController = GameObject.FindObjectOfType<UIController>();

            currenthealth = maxhealth;

            playerhealthBar.FindHealthBarSprite();

            healthBar = GameObject.Find("UIController").gameObject.transform.Find("HealthBar").GetComponent<HealthBar>();
            SpawnPointOnEnterLevel = GameObject.Find("PlayerSpawnPoint").gameObject.transform.position;

            transform.position = SpawnPointOnEnterLevel;
        }
    
    }

    #endregion
}
