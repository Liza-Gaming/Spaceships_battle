using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sounds;

    public AudioClip shoot;
    public AudioClip hit;
    public AudioClip lose;
    public void PlaySFX(AudioClip clip)
    {
        sounds.PlayOneShot(clip);
    }

}
