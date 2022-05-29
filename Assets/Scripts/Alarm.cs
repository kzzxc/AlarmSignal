using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _speedOfChanging;

    private AudioSource _alarmSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Coroutine _currentCoroutine;
    private bool _isActivated;

    private void Start()
    {
        _alarmSource = GetComponent<AudioSource>();
    }
    
    public void PlayerEntred()
    {
        _isActivated = !_isActivated;

        if (_isActivated)
        {
            TryStopCoroutine(_currentCoroutine);

            _currentCoroutine = (StartCoroutine(PlayAlarm()));
        }
        else
        {
            TryStopCoroutine(_currentCoroutine);

            _currentCoroutine = (StartCoroutine(StopAlarm()));
        }
    }

    private void TryStopCoroutine(Coroutine currentCoroutine)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }

    private float ChangeVolume()
    {
        return Mathf.MoveTowards(_minVolume, _maxVolume, _speedOfChanging * Time.deltaTime);              
    }

   private IEnumerator PlayAlarm()
    {
        _alarmSource.Play();

        while (_alarmSource.volume < _maxVolume)
        {
            _alarmSource.volume += ChangeVolume();

            yield return null;
        }
    }

    private IEnumerator StopAlarm()
    {
        while (_alarmSource.volume > _minVolume)
        {
            _alarmSource.volume -= ChangeVolume();

            yield return null;
        }

        if(_alarmSource.volume == _minVolume)
        _alarmSource.Stop();
    }
}
