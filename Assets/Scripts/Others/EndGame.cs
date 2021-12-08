using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void RestartGame()
    {
        SceneController.LoadScene("MainMenu", 1f, 1f);
    }
       
}
