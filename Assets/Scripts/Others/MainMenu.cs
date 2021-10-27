using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject Credits;

    #endregion

    #region PUBLIC_FUNCTIONS

    public void StartGame()
    {
        SceneController.LoadScene(1, 1f, 0.5f);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

    #region PRIVATE_FUNCTION
    private void OpenWindow(GameObject window)
    {
        window.SetActive(true);
    }
    private void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
    
    #endregion
}
