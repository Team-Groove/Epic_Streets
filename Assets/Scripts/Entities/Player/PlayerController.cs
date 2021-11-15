using UnityEngine;

public class PlayerController : Fighter
{
    #region VARIABLES

    #endregion

    #region UNITY_CALLS

    private void Update()
    {
        UpdateHealthBar(currenthealth);
        IfCurrentLifeisZero();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChangeScene"))
        {
            SceneController.LoadScene("Gameplay2", 1f, 0.5f);
        }
        else if (collision.gameObject.CompareTag("ChangeScene2"))
        {
            SceneController.LoadScene("MainHub", 1f, 0.5f);
        }
        else if (collision.gameObject.CompareTag("RepeatScene"))
        {
            SceneController.LoadScene(SceneController.GetActualScene(), 1f, 0.5f);
        }
    }

    #endregion

   
}
