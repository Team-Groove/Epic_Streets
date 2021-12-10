using UnityEngine;
using UnityEngine.UI;
public class AudioSettings : MonoBehaviour
{
    public static float music;
    public static float effect;

    [SerializeField] private Slider effects_s;
    [SerializeField] private Slider music_s;

    private void Start()
    {
        music_s.value = 1f;
        effects_s.value = .5f;
        music = 1;
        effect = .5f;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        music = music_s.value;
        effect = effects_s.value;
    }
}
