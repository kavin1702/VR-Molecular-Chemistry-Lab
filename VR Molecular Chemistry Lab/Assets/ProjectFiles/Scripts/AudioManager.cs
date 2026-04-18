using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip successClip;

    public void PlaySuccess()
    {
        source.PlayOneShot(successClip);
    }
}   