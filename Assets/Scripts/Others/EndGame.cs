using UnityEngine;

public class EndGame : MonoBehaviour
{

    SceneController sceneController;

    private void Start()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        sceneController.endGameNum += 1;
    }

    public void RestartGame()
    {
        SceneController.LoadScene("MainHub", 1f, 1f);
    }

    public void Quit()
    {
        Application.Quit();
    }

   
}
