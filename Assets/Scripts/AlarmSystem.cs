using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _recoveryRateSoundOn;
    [SerializeField] private float _recoveryRateSoundOff;
    [SerializeField] private float _maxSoundVolume;

    private bool _isPlayerInHouse = false;
    private float _soundVolume;
    private readonly float _minSoundVolume = 0;

    private void Awake()
    {
        if(_maxSoundVolume >= 1)
        {
            _maxSoundVolume = 1;
        }
        if (_maxSoundVolume <=0)
        {
            _maxSoundVolume = 0;
        }

        _soundVolume = _minSoundVolume;
    }

    private void Update()
    {
        if (_isPlayerInHouse)
        {
            SoundVolumeMoveTowards(_maxSoundVolume, _recoveryRateSoundOn);
        }
        else
        {
            SoundVolumeMoveTowards(_minSoundVolume, _recoveryRateSoundOff);
        }
        
        _audio.volume = _soundVolume;
    }

    private void SoundVolumeMoveTowards(float targetSoundVolume, float recoveryRate)
    {
        _soundVolume = Mathf.MoveTowards(_soundVolume, targetSoundVolume, recoveryRate * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            _isPlayerInHouse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isPlayerInHouse = false;
        }
    }
}
