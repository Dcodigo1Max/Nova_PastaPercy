using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;//Sound effects

    [SerializeField] float pitchVariance = 0.15f;

    

    //The music/soundeffects we want in the game
    [Header("------Audio Clip--------")]
    public AudioClip background;
    public AudioClip Water;
    public AudioClip Bushes;
    public AudioClip Waterattack;
    public AudioClip BgSwordFighting;
    public AudioClip Running;
    public AudioClip Jumping;
    public AudioClip SwordAttack;
    public AudioClip Damage;
    public AudioClip Death;

    //private bool Run = false;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        
    }
    /*public void LoopSFX(AudioClip clip, float volume = 1f)
    {
        
        
        if (!Run )
        {
            float randomPitch = Random.Range(1 - pitchVariance, 1 + pitchVariance);
            SFXSource.pitch = randomPitch;
            SFXSource.loop = true;
            SFXSource.Play();
            Run = true;
        }
    }
    public void StopLoopSFX(AudioClip clip, float volume = 1f)
    {
        SFXSource.Stop();
        SFXSource.loop = false;
        Run = false;
    }*/
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        float randomPitch = Random.Range(1 - pitchVariance, 1 + pitchVariance);
        SFXSource.pitch = randomPitch;
        SFXSource.PlayOneShot(clip,volume);
    }

}
