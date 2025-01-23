using System;
using UnityEngine;

public enum SoundType
{
    MenuTheme,
    BurstSFX,
    BounceSFX,
    IcePUSFX,
    TPSFX,
    WindSFX,
    LvlTheme
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Values")]
    [SerializeField] private float _musicVolume = 1.0f;
    [SerializeField] private float _sfxVolume = 1.0f;

    private AudioSource _audioSource1;
    private AudioSource _audioSource2;
    private AudioSource _sfxSource;
    private SoundType _soundType;

    [SerializeField] private AudioClip[] _soundList;

    [SerializeField] private bool _firstAudioSourceIsPlaying;

    [SerializeField] private bool _menuScene;

    public SoundType SoundType => _soundType;

    public static AudioManager Instace
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();

                if(instance == null)
                {
                    instance = new GameObject("AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }

        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            _audioSource1 = this.gameObject.AddComponent<AudioSource>();
            _audioSource2 = this.gameObject.AddComponent<AudioSource>();
            _sfxSource = this.gameObject.AddComponent<AudioSource>();

            _audioSource1.loop = true;
            _audioSource2.loop = true;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(_menuScene == true) PlayMusic(SoundType.MenuTheme, 1);
        else PlayMusic(SoundType.LvlTheme, 0);
    }

    public float GetMusicVolume()
    {
        return _musicVolume;
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = Mathf.Clamp01(volume);
        _audioSource1.volume = _musicVolume;
        _audioSource2.volume = _musicVolume;
    }

    public float GetSFXVolume()
    {
        return _sfxVolume;
    }

    public void SetSFXVolume(float volume)
    {
        _sfxVolume = Mathf.Clamp01(volume);
        _sfxSource.volume = _sfxVolume;
    }

    public void PlayMusic(SoundType soundType, float volume)
    {
        AudioSource activeSource = _firstAudioSourceIsPlaying ? _audioSource1 : _audioSource2;
        activeSource.clip = _soundList[(int)soundType];
        activeSource.volume = _musicVolume;
        activeSource.Play();
    }

    public void PlaySFX(SoundType soundType, float volume)
    {
        AudioClip clip = _soundList[(int)soundType];

        if (clip != null)
        {
            _sfxSource.PlayOneShot(clip, volume * _sfxVolume);
        }
        else
        {
            Debug.LogWarning("Audio clip not found for sound type: " + soundType);
        }
    }

    public void ChangeMusic(SoundType newSoundType, float volume)
    {
        AudioSource activeSource = _firstAudioSourceIsPlaying ? _audioSource1 : _audioSource2;

        activeSource.Stop();

        activeSource.clip = _soundList[(int)newSoundType];
        activeSource.volume = volume;
        activeSource.Play();
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref _soundList, names.Length);

        for (int i = 0; i < _soundList.Length; i++)
        {
            _soundList[i].name = names[i];
        }
    }
#endif
}


