using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : GenericSingleton<MySceneManager>
{
    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name.ToLower())
        {
            case "MainMenu":
                // Do some "menu" initialisation here...
                break;

            case "game":
                // Do some "game" initialisation here...
                break;

            case "gameover":
                // Do some "gameover" initialisation here...
                break;
        }
    }

    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    protected override void InternalInit()
    {
        
    }
}

