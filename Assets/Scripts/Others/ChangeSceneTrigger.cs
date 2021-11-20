using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneController.LoadScene("Gameplay", 0.5f, 1f);
        }
    }
    
    public void ChangeSceneTo(string name)
    {
        SceneController.LoadScene(name, 0.5f, 1f);
    }
   


}
