using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIInputController : MonoBehaviour
{
    #region VARIABLES

    //PRIVADAS
    [SerializeField] private Image TabMenu;
    [SerializeField] private Image pauseMenu;

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
        OnPauseGame();
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
        if (context.performed)
        {
            TabMenu.gameObject.SetActive(true);
            tabMenuOn = true;
        }
        else
        {
            TabMenu.gameObject.SetActive(false);
            tabMenuOn = false;
        }
    }
    public void BackToMenu()
    {
        gameInPause = false;
        SceneController.LoadScene(0, 1f, 0.5f);
    }

    #endregion
}
