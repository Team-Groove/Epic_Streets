using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    public bool repeatScene;
    public bool toGameplay;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("aAGadg");
        
        if (collision.gameObject.tag == "Player")
        {

            SceneController.LoadScene("Gameplay", 0.5f, 1f);

        }
    }
    
    public void ChangeSceneTo(string name)
    {
        SceneController.LoadScene(name, 0.5f, 1f);
    }
    public void ChangeSceneToGameplay()
    {
        Time.timeScale = 1;
        SceneController.LoadScene("Gameplay", 0.5f, 1f);
    }


}
