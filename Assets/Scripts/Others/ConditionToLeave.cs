using UnityEngine;

public class ConditionToLeave : MonoBehaviour
{
    public bool playerCanLeave;

    [SerializeField] private GameObject[] enemies;

    private void Update()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                playerCanLeave = true;
            }
        }
    }

}
