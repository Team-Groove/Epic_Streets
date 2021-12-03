using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    #region VARIABLES


    //PRIVADAS
    [SerializeField] public Image TabMenu;
    [SerializeField] private Image pauseMenu;
    [SerializeField] private Image[] comboSlots;

    public Image gameOverScreen;

    public bool gameInPause;
    
    //VARIABLES ESTATICAS
    public bool tabMenuOn;

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
    public void SetPauseGameFalse()
    {
        gameInPause = false;
    }
    #endregion
}
