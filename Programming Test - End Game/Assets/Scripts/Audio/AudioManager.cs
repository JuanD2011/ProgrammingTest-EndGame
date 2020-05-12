using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource sfxSource = null;
    [SerializeField] private AudioSource musicSource = null;
    
    protected override void OnAwake()
    {
        SetVolume();
        PlayDefaultMusic();
    }

    private void SetVolume()
    {
        Instance.sfxSource.volume = AudioSettings.Instance.sfxVolume;
        Instance.musicSource.volume = AudioSettings.Instance.musicVolume;
    }

    public static void PlaySFX(AudioClip _clip)
    {
        if (_clip != null)
        {
            Instance.sfxSource.pitch = Random.Range(AudioSettings.Instance.pitchVariationRange.x, AudioSettings.Instance.pitchVariationRange.y);
            Instance.sfxSource.PlayOneShot(_clip); 
        }
    }

    public static void PlayDefaultMusic()
    {
        if (MusicAudioClips.Instance.defaultMusic != null)
        {
            Instance.musicSource.clip = MusicAudioClips.Instance.defaultMusic;
            Instance.musicSource.Play(); 
        }
    }
}
