using UnityEngine;
using UnityEngine.UI;
public class AudioSettings : MonoBehaviour
{
    public static float music;
    public static float effect;

    private string audioVolumeString = "AudioVolume";
    private string audioMusicString = "AudioMusic";

    [SerializeField] private Slider effects_s;
    [SerializeField] private Slider music_s;

    public bool firstRun;

    private void Awake()
    {
        LoadData();
    }

    private void Start()
    {
        if (firstRun)
        {
            music_s.value = 1f;
            effects_s.value = .5f;
            music = 1;
            effect = .5f;
            gameObject.SetActive(false);
            firstRun = false;
        }
    }

    private void Update()
    {
        music = music_s.value;
        effect = effects_s.value;
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(audioMusicString, music_s.value);
        PlayerPrefs.SetFloat(audioVolumeString, effects_s.value);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(audioMusicString))
        {
            music_s.value = PlayerPrefs.GetFloat(audioMusicString, 1f);
        }
        else
        {
            music_s.value = 1f;
            PlayerPrefs.SetFloat(audioMusicString, 1f);
        }

        if (PlayerPrefs.HasKey(audioVolumeString))
        {
            effects_s.value = PlayerPrefs.GetFloat(audioVolumeString, .5f);
        }
        else
        {
            effects_s.value = .5f;
            PlayerPrefs.SetFloat(audioVolumeString, .5f);
        }

    }

    private void OnDestroy()
    {
        SaveData();
    }
}
