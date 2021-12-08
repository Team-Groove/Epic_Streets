using UnityEngine;

public class NPCsounds : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip hit3;
    public AudioClip fire;
    public void PlaySound1()
    {
        audioSource.PlayOneShot(hit1);
    }

    public void PlaySound2()
    {
        audioSource.PlayOneShot(hit2);
    }

    public void PlaySound3()
    {
        audioSource.PlayOneShot(hit3);
    }

    public void PlayFireSound()
    {
        audioSource.PlayOneShot(fire);
    }
}
