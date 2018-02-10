using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffects : MonoBehaviour 
{
    // serializables

    [SerializeField]
    ParticleSystem _particleSystem;

    [SerializeField]
    Light _light;

    [SerializeField]
    WeaponSound _soundPrefab;



    // field

    private List<WeaponSound> _sounds;

    private float _delta;



    // MonoBehaviour

    private void Awake()
    {
        _sounds = new List<WeaponSound>();
    }

    void Start()
    {
        if(_light != null)
        {
            float duration = 0.3f;

            if(_particleSystem != null)
            {
                duration = _particleSystem.main.startLifetime.constant;
            }
            else if(_soundPrefab != null)
            {
                duration = _soundPrefab.Length;
            }

            _delta = 1.0f / duration;
        }
    }

    void Update()
    {
        if(_light != null)
        {
            if (_light.color.a > 0)
            {
                Color color = _light.color;
                color.a -= _delta * Time.deltaTime;
                _light.color = color;
            }
            else
            {
                SetLightAlpha(0);

                _light.enabled = false;
            }
        }
    }
    


    // WeaponEffects

    public void Play ()
    {
        if (_light != null)
        {
            SetLightAlpha(1);

            _light.enabled = true;
        }
       
        if(_particleSystem != null)
        {
            _particleSystem.Play ();   
        }

        if(_soundPrefab != null)
        {
            if (_sounds.Count > 0)
            {
                _sounds[0].gameObject.SetActive(true);
                _sounds.RemoveAt(0);
            }
            else
            {
                WeaponSound sound = Instantiate(_soundPrefab.gameObject).GetComponent<WeaponSound>();
                sound.OnSoundFinished += OnSoundFinished;
            }
        }
    }

    private void OnSoundFinished(WeaponSound sound)
    {
        sound.gameObject.SetActive(false);

        _sounds.Add(sound);
    }

    void SetLightAlpha(float alpha)
    {
        Color color = _light.color;
        color.a = 1;

        _light.color = color;
    }
}