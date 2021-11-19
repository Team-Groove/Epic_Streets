using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIInputController : MonoBehaviour
{
    #region VARIABLES

    //PRIVADAS
    [SerializeField] private Image TabMenu;
    [SerializeField] private Image pauseMenu;
    [SerializeField] private Image[] comboSlots;
    
    private bool gameInPause;
    
    //VARIABLES ESTATICAS
    public static bool tabMenuOn;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        TabMenu.gameObject.SetActive(false);
    }

    private void Update()
    {
        // TODO: remove this constant checking
        OnPauseGame(); // PAUSA DESHABILITADA
        ComboSlotsHideImages();
    }

    #endregion
   
    #region PRIVATE_FUNCTIONS
    private void OnPauseGame()
    {
        if (gameInPause)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    private void ComboSlotsHideImages()
    {

        if (tabMenuOn)
        {
            for (int i = 0; i < comboSlots.Length; i++)
            {
                comboSlots[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < comboSlots.Length; i++)
            {
                comboSlots[i].enabled = false;
            }
        }
    }
    #endregion

    #region PUBLIC_FUNCTIONS
    public void PauseInput(InputAction.CallbackContext context)
    {
        if (context.performed && !gameInPause)
        {
            gameInPause = true;
        }
        else if (context.performed && gameInPause)
        {
            gameInPause = false;
        }
    }
    public void ComboMenuInput(InputAction.CallbackContext context)
    {
        if (context.performed && !tabMenuOn)
        {
            TabMenu.gameObject.SetActive(true);
            tabMenuOn = true;
        }
        else if (context.performed && tabMenuOn)
        {
            TabMenu.gameObject.SetActive(false);
            tabMenuOn = false;
        }
    }
    public void BackToMenu()
    {
        gameInPause = false;
        SceneController.LoadScene("MainMenu", 1f, 0.5f);
    }
    public void ChangeSceneToGameplay()
    {
        gameInPause = false;
        SceneController.LoadScene("Gameplay", 1f, .5f);
    }
    #endregion
}
