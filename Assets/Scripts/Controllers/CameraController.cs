using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private CinemachineConfiner confiner;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public bool doNotFollowPlayer;

    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();
    }
    private void Start()
    {
        if (!SceneManager.GetSceneByName("MainMenu").isLoaded && !SceneManager.GetSceneByName("EndGame").isLoaded && !doNotFollowPlayer)
        {
        virtualCamera.Follow = FindObjectOfType<PlayerController>().transform;
        confiner.m_ConfineMode = CinemachineConfiner.Mode.Confine2D;
        confiner.m_BoundingShape2D = GameObject.Find("CameraWorldBounderies").GetComponent<PolygonCollider2D>();

        }

        if (doNotFollowPlayer)
        {

        }

    }

    private void Update()
    {

        if (!SceneManager.GetSceneByName("MainMenu").isLoaded && !SceneManager.GetSceneByName("EndGame").isLoaded && !doNotFollowPlayer)
        {
            if (virtualCamera.m_Follow == null)
            {
                virtualCamera.m_Follow = FindObjectOfType<PlayerController>().gameObject.transform;
            }
        }
        else return;
    }

}
