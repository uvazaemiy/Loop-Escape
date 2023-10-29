using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    [SerializeField] private SoundButtonData fxButton;
    [SerializeField] private SoundButtonData musicButton;
    [Space]
    [Range(0, 1)] 
    [SerializeField] private float musicVolume = 0.5f;
    [Range(0, 1)] 
    [SerializeField] private float fxVolume;
    [Range(0.7f, 1)]
    [SerializeField] private float lowPitch = 0.8f;
    [Range(1, 1.3f)]
    [SerializeField] private float highPitch = 1.2f;

    [SerializeField] private AudioClip[] winSounds;
    [SerializeField] private AudioClip[] loseSounds;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip leverSound;
    [SerializeField] private AudioClip wallSound;

    private void Start()
    {
        instance = this;
        
        if (PlayerPrefs.GetFloat("fxVolume") == -1)
			ChangeFxVolume(fxButton);
        if (PlayerPrefs.GetFloat("MusicVolume") == -1)
            ChangeMusicVolume(musicButton);
    }

    public AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1, bool steps = false, bool pitch = true)
    {
        if (clip != null)
        {
            GameObject go = new GameObject("SoundFX " + clip.name);
            go.transform.position = position;

            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;

            float randomPitch = Random.Range(lowPitch, highPitch);
            if (pitch)
                source.pitch = randomPitch;
            source.volume = volume;

            source.Play();
            if (!steps)
                Destroy(go, clip.length);
            return source;
        }

        return null;
    }

    private AudioSource PlayRandom(AudioClip[] clips, Vector3 position, float volume = 1)
    {
        if (clips != null)
        {
            if (clips.Length != 0)
            {
                int randomIndex = Random.Range(0, clips.Length);

                if (clips[randomIndex] != null)
                {
                    AudioSource source = PlayClipAtPoint(clips[randomIndex], position, volume);
                    return source;
                }
            }
        }

        return null;
    }

    public void PlayWinSound()
    {
        PlayRandom(winSounds, Vector3.zero, fxVolume);
    }
    
    public void PlayLoseSound()
    {
        PlayRandom(loseSounds, Vector3.zero, fxVolume);
    }

    public void PlayDeathSound()
    {
        PlayClipAtPoint(deathSound, Vector3.zero, fxVolume);
    }
    
    public void PlayLeverSound()
    {
        PlayClipAtPoint(leverSound, Vector3.zero, fxVolume);
    }    
    
    public void PlayWallSound()
    {
         PlayClipAtPoint(wallSound, Vector3.zero, fxVolume);
    }

    public void ChangeMusicVolume(SoundButtonData soundButtonData)
    {
        musicVolume = soundButtonData.ChangeVolume(musicVolume);
        MusicPlayer.instance.music.volume = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void ChangeFxVolume(SoundButtonData soundButtonData)
    {
        fxVolume = soundButtonData.ChangeVolume(fxVolume);
        PlayerPrefs.SetFloat("fxVolume", fxVolume);
    }
}