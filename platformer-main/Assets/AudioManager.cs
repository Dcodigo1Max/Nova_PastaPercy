using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;//Sound effects

    //The music/soundeffects we want in the game
    [Header("------Audio Clip--------")]
    public AudioClip background;
    public AudioClip Water;
    public AudioClip BgSwordFighting;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
