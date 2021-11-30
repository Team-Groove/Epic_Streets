using UnityEngine;

public class HealthBarRedFeedback : MonoBehaviour
{

    public static HealthBarRedFeedback instance { get; private set; }
    private Animator animator;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        animator.Play("HealthBarHurt");
    }
}
