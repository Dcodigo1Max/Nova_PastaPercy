using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;//Sound effects

    //The music/soundeffects we want in the game
    [Header("------Audio Clip--------")]
    public AudioClip background;
    public AudioClip Water;
    public AudioClip Bushes;
    public AudioClip Waterattack;
    public AudioClip BgSwordFighting;
    public AudioClip Running;
    public AudioClip Jumping;

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
