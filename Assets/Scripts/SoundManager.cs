using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Main;

    public SoundResources resource;

    AudioSource source;

    [SerializeField]
    [Range(0, 1)]
    private float volume;
    public AudioClip actuallClip;

    public AudioClip backgroundJumpAndRUn;
    public AudioClip backgroundBilderRaten;

    [SerializeField]
    [Range(0, 1)]
    private float volumeBackground;

    private List<AudioSource> allSFXsources = new List<AudioSource>();

    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Main != this) Destroy(this);
    }

    private void Start()
    {
        actuallClip = backgroundJumpAndRUn;
        source = gameObject.AddComponent<AudioSource>();
        source.clip = actuallClip;
        source.loop = true;
        source.volume = volumeBackground;
    }

    private void Update()
    {
        List<AudioSource> finishedPlaying = new List<AudioSource>();

        foreach (AudioSource source in allSFXsources)
        {
            if (!source.isPlaying)
            {
                finishedPlaying.Add(source);
            }
        }

        foreach (AudioSource source in finishedPlaying)
        {
            allSFXsources.Remove(source);
            Destroy(source);
        }
    }

    public void PlayNewSound(AudioClip clip, bool loop = false)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        Debug.Log("play");
        source.clip = clip;
        source.loop = loop;
        if (source == resource.playerJump) source.volume = 0.2f;
        else source.volume = volume;
        source.Play();
        allSFXsources.Add(source);
    }

    public void ChooseSound(SoundType type, bool loop = false)
    {
        AudioClip clip = null;
        switch (type)
        {
            case SoundType.PlayerJump:
                clip = resource.playerJump;
                break;
            case SoundType.PlayerDie:
                clip = resource.playerDie;
                break;
            case SoundType.PlayerCollect:
                clip = resource.playerCollect;
                break;
            case SoundType.cheer:
                clip = resource.cheer;
                break;
            case SoundType.oohh:
                clip = resource.oohh;
                break;

        }
        PlayNewSound(clip, loop);
    }

    public void PlayBackground(AudioClip clip)
    {
        source.Stop();
        actuallClip = clip;

        source.clip = actuallClip;
        source.loop = true;
        source.volume = volumeBackground;
        source.Play();
    }

    public void StopBackgroundMusic()
    {
        source.Stop();
    }

    public void ChangeBackGroundMusic(BackgroundMusic audio)
    {
        switch (audio)
        {
            case BackgroundMusic.backgroundJumpAndRUn:
                PlayBackground(backgroundJumpAndRUn);
                break;
            case BackgroundMusic.backgroundBilderRaten:
                PlayBackground(backgroundBilderRaten);
                break;
        }
    }
}

public enum BackgroundMusic
{
    backgroundJumpAndRUn,
    backgroundBilderRaten
}

public enum SoundType
{
    PlayerJump,
    PlayerDie,
    PlayerCollect,
    cheer,
    oohh
}
