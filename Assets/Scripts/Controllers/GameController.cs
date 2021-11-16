using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Image gameOverScreen;

    
    public bool gameOver = false;

    public float gameOverFadeInTime;

    #endregion

    #region UNIT_CALLS

    private void Start()
    {
        gameOver = false;
    }

    private void Awake()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
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
        if (playerController.currenthealth <= 0)
        {
            gameOver = true;
        }
    }
    
    private void GameOverSecuence()
    {
        if (gameOver)
        {
            StartCoroutine("GameOverCoroutine");
           
        }
    }

    private IEnumerator GameOverCoroutine()
    {
        
        gameOverScreen.gameObject.SetActive(true);

        for (float t = 0; t < 1; t += Time.deltaTime / gameOverFadeInTime) //DURACION DEL FADE IN
        {
            gameOverScreen.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        SceneController.LoadScene("MainMenu", 2f, 1f);
    }


    #endregion

}
