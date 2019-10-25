using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioSource loopSFX;

    public static AudioPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayBGM(AudioClip _musica)
    {
        BGM.clip = _musica;
        BGM.Play();
    }

    public void PlaySFX(AudioClip _efeitoSonoro)
    {
        SFX.PlayOneShot(_efeitoSonoro);
    }

    public void PlayLoopSFX(AudioClip _efeitoSnoro)
    {
        loopSFX.clip = _efeitoSnoro;
        loopSFX.Play();
    }

    public void StopBGM()
    {
        BGM.Stop();
    }

    public void StopSFX()
    {
        SFX.Stop();
    }

    public void StopLoopSFX()
    {
        loopSFX.Stop();
    }

    public void PauseLoopSFX()
    {
        loopSFX.Pause();        
    }

    public void UnpauseLoopSFX()
    {
        loopSFX.UnPause();
    }
}
