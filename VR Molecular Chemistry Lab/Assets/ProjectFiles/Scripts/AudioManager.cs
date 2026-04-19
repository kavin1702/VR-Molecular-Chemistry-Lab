using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip grabClip;
    public AudioClip successClip;

    private void Awake()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }

    public void PlayGrab()
    {
        source?.PlayOneShot(grabClip);
    }

    public void PlaySuccess()
    {
        source?.PlayOneShot(successClip);
    }
}