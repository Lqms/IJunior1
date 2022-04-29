using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _currentVolume = 0f;
    [SerializeField] private float _volumeRate = 0.1f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _targetVolume;

    public void TurnOn()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        _targetVolume = _maxVolume;
    }

    public void TurnOff()
    {
        _targetVolume = _minVolume;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _currentVolume = _audioSource.volume;
        _audioSource.volume = Mathf.MoveTowards(_currentVolume, _targetVolume, _volumeRate * Time.deltaTime);

        if (_audioSource.volume <= 0f && _audioSource.isPlaying == true)
            _audioSource.Stop();
    }
}
