using UnityEngine;

public class EndGame : MonoBehaviour
{

    SceneController sceneController;

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        sceneController.endGameNum += 1;
    }

    public void RestartGame()
    {
        SceneController.LoadScene("MainHub", 1f, 1f);
    }
    
    private void SaveData()
    {
  
    }

    private void OnDestroy()
    {
        SaveData();
    }
}
