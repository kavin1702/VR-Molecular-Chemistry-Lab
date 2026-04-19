//using UnityEngine;

//public class AudioManager : MonoBehaviour
//{
//    public AudioSource source;
//    public AudioClip successClip;

//    public void PlaySuccess()
//    {
//        source.PlayOneShot(successClip);
//    }
//}   

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip grabClip;
    public AudioClip successClip;

    public void PlayGrab()
    {
        source.PlayOneShot(grabClip);
    }

    public void PlaySuccess()
    {
        source.PlayOneShot(successClip);
    }
}