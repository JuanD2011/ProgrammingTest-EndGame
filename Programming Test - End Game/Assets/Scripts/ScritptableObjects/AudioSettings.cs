using UnityEngine;

[CreateAssetMenu]
public class AudioSettings : ScriptableSingleton<AudioSettings>
{
    [Header("Volume")]
    [Range(0, 1)] public float musicVolume;
    [Range(0, 1)] public float sfxVolume;

    [Header("SFX Parameters")]
    public Vector2 pitchVariationRange;
    //Add other parameters
}
