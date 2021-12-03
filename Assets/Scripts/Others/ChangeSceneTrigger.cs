using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{

    public ConditionToLeave conditionToLeave;
    public string sceneName;

    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        conditionToLeave = GameObject.Find("ConditionToLeave").GetComponent<ConditionToLeave>();
        boxCollider.isTrigger = false;
    }

    private void Update()
    {
        if (conditionToLeave.playerCanLeave == true)
        {
            boxCollider.isTrigger= true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneController.LoadScene(sceneName, 0.5f, 1f);
        }
    }

}
