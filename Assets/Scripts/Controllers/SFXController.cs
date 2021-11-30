using UnityEngine;


public class SFXController : MonoBehaviour
{
    public static SFXController instance = null;

    [SerializeField] private AudioSource trackSource = null;
    [SerializeField] public AudioSource enemyHitBox = null;
    [SerializeField] public AudioSource playersAttack = null;


    [SerializeField] private AudioClip[] tracks = null;

    [SerializeField] private float[] modeVolumes = null;

    public enum VOLUME_MODE { MINIMAL, NORMAL, HIGH }
    public enum PITCH_MODE { MINIMAL, NORMAL, HIGH }
    public enum TRACK { MENU, GAMEPLAY }


    #region UNITY_CALLS
    private void Awake()
    {
        instance = this;
    }

    #endregion

    #region PUBLIC_METHODS
    public void SetVolumeInstant(int mode)
    {
        trackSource.volume = modeVolumes[mode];
    }

    public void SetTrack(int track)
    {
        trackSource.clip = tracks[track];
        trackSource.Play();
    }

    public void PlayOnHitEnemySound(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        enemyHitBox.volume = volume;
        enemyHitBox.pitch = pitch;

        enemyHitBox.PlayOneShot(clip);
    }
    public void PlayOnPlayerAttackSound(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        playersAttack.volume = volume;
        playersAttack.pitch = pitch;

        playersAttack.clip = clip;

        playersAttack.Play();
    }


    #endregion

    #region PRIVATE_METHODS
    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StopMusic()
    {
        trackSource.Stop();
    }

    public void SwitchMusic(bool status)
    {
        if (status)
        {
            trackSource.Play();
        }
        else
        {
            if (trackSource.isPlaying)
            {
                trackSource.Pause();
            }
            else
            {
                trackSource.Stop();
            }
        }


    }
    #endregion
}