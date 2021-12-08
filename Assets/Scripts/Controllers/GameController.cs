using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region VARIABLES

    public PlayerController playerController;
    private Image gameOverScreen;
  
    public bool gameOver = false;

    public float gameOverFadeInTime;

    public bool runOnce;

    #endregion

    #region UNIT_CALLS
    
    
    private void Start()
    {
        gameOverFadeInTime = 3f;

        gameOver = false;
        playerController = FindObjectOfType<PlayerController>();
        gameOverScreen = GameObject.Find("UIController").gameObject.transform.Find("GameOverScreen").GetComponent<Image>();
    }

    private void Update()
    {
        PlayersLifeisZero();
        GameOverSecuence(); 
    }

    #endregion

    #region FUNCTIONS

    private void PlayersLifeisZero()
    {
        if (playerController == null)
        {
            return;
        }
        else if (playerController.currenthealth <= 0)
        {
            gameOver = true;
            playerController.IsDead = true;
        }
    }
    private void GameOverSecuence()
    {
        if (gameOver)
        {
            if (runOnce == false)
            {
                StartCoroutine("GameOverCoroutine");
                gameOver = false;
                runOnce = true;
            }
        }
    }
    private IEnumerator GameOverCoroutine()
    {
        AudioManager.instance.StopAllMusic();
        AudioManager.instance.PlayMusic("GameOver");
        gameOverScreen.gameObject.SetActive(true);

        for (float t = 0; t < 1; t += Time.deltaTime / gameOverFadeInTime) //DURACION DEL FADE IN
        {
            gameOverScreen.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        SceneController.LoadScene("MainHub", 2f, 1f);
    }
  
    private void CheckIfObjectExistInScene(string objectToFind, GameObject objectToInstantiate)
    {
        if (GameObject.Find(objectToFind) != null)
        { 
            return;
        }
        else
        {
            Instantiate(objectToInstantiate, GameObject.Find("PlayerSpawnPoint").GetComponent<Transform>().transform.position, Quaternion.identity);
        }
    }
    #endregion

}
