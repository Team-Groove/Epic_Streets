using UnityEngine;

public class PlayerController : Fighter
{
    #region VARIABLES

    #endregion

    #region UNITY_CALLS

    private void Update()
    {
        UpdateHealthBar(currenthealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChangeScene"))
        {
            SceneController.LoadScene(2, 1f, 0.5f);
        }
        if (collision.gameObject.CompareTag("ChangeScene2"))
        {
            SceneController.LoadScene(3, 1f, 0.5f);
        }
    }

    #endregion
}
