using UnityEngine;

public class WeaponSound : MonoBehaviour 
{
    // delegate

    public delegate void SoundFinished(WeaponSound instance);

    public SoundFinished OnSoundFinished;



    // properties

    public float Length
    {
        get
        {
            return _audioSource != null && _audioSource.clip != null ?_audioSource.clip.length : 0;
        }
    }



    // fields

    AudioSource _audioSource;



    // MonoBehaviour

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
	
    void OnEnable()
    {
        _audioSource.Play();
    }

    void Update()
    {
        if(!_audioSource.isPlaying)
        {
            if(OnSoundFinished != null)
            {
                OnSoundFinished.Invoke(this);
            }
        }
    }
}