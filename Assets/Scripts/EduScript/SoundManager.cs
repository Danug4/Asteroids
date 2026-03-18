using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] collectionSounds;
    public AudioClip[] explosionSounds;
    public AudioClip[] bulletSounds;
    public AudioClip[] deathSounds;
    public AudioClip[] impactSounds;
    public AudioSource[] sfxAudioSources;

    public void PlayRandomSound(AudioClip[] _clips)
    {
        float minPitch = 0.8f;
        float maxPitch = 1.2f;

        AudioSource availableAudioSource = GetFreeAudioSource(sfxAudioSources);
        int rnd = Random.Range(0, _clips.Length);
        availableAudioSource.clip = _clips[rnd];
        availableAudioSource.pitch = Random.Range(minPitch, maxPitch);

        availableAudioSource.Play();
    }

    public AudioSource GetFreeAudioSource(AudioSource[] sources)
    {
        for (int i = 0; i < sources.Length; i++)
        {
            if (!sources[i].isPlaying)
            {
                return sources[i];
            }
        }
        return null; // If none are available
    }
}
