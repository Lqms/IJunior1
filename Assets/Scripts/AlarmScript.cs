using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmScript : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _currentVolume = 0f;
    [SerializeField] private float _volumeRate = 0.1f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _targetVolume;

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

    public void TurnOnAlarm()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        _targetVolume = _maxVolume;
        Debug.Log("turn on");
    }
    public void TurnOffAlarm()
    {
        _targetVolume = _minVolume;
        Debug.Log("turn off");
    }
}
