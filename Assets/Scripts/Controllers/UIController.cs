using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    #region VARIABLES

    public static UIController instance;

    //PRIVADAS
    [SerializeField] public Image TabMenu;
    [SerializeField] private Image pauseMenu;
    [SerializeField] private Image[] comboSlots;
 

    private Image settingsMenu;

    public Image gameOverScreen;

    public bool gameInPause;

    //PlayerPref posiciones



    //VARIABLES ESTATICAS
    public bool tabMenuOn;

    #endregion

    #region UNITY_CALLS
    private void Awake()
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

         DontDestroyOnLoad(gameObject);
    }
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            gameObject.SetActive(false);
        }
       /* if (!SceneManager.GetSceneByName("MainMenu").isLoaded && !SceneManager.GetSceneByName("MainHub").isLoaded)
        {
            gameObject.SetActive(true);
        }*/

        if (SceneManager.GetSceneByName("MainHub").IsValid())
        {
            gameObject.SetActive(true);
            gameOverScreen.gameObject.SetActive(false);
        }

    }

    #endregion
}
