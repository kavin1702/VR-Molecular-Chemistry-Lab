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

        if (source == null)
            Debug.LogError("❌ AudioSource missing on AudioManager!");
    }

    public void PlayGrab()
    {
        if (source != null && grabClip != null)
        {
            Debug.Log("🔊 Grab sound");
            source.PlayOneShot(grabClip);
        }
    }

    public void PlaySuccess()
    {
        if (source != null && successClip != null)
        {
            Debug.Log("🔊 Success sound");
            source.PlayOneShot(successClip);
        }
    }
}