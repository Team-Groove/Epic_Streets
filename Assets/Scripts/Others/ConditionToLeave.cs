using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class ConditionToLeave : MonoBehaviour
{
    public bool playerCanLeave;

    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    private GameObject Go;

    private void Start()
    {
        if (!SceneManager.GetSceneByName("MainMenu").isLoaded && !SceneManager.GetSceneByName("MainHub").isLoaded)
        {
            Go = GameObject.Find("LevelUI").gameObject.transform.Find("Go!").gameObject;
            Go.gameObject.SetActive(false);
        }
        else
        {
            playerCanLeave = true;
            Go = null;
        }
    }

    private void Update()
    {
        if (!SceneManager.GetSceneByName("MainMenu").isLoaded && !SceneManager.GetSceneByName("MainHub").isLoaded)
        {
            for (var i = enemies.Count - 1; i > -1; i--)
            {
                if (enemies[i] == null)
                    enemies.RemoveAt(i);
            }


            if (enemies.Count == 0)
            {
                playerCanLeave = true;
                Go.gameObject.SetActive(true); 
            }
        }

       
    }

}
