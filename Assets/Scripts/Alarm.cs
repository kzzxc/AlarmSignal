using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private House _house;
    [SerializeField] private float _speedOfChanging;

    private AudioSource _alarmSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _alarmSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _house.PlayerEntred += OnPlayerEntred;
    }

    private void OnDisable()
    {
        _house.PlayerEntred -= OnPlayerEntred;
    }

    private void OnPlayerEntred(bool isEntred)
    {
        if (isEntred)
            _currentCoroutine = StartCoroutine(PlayAlarm());
        else
            _currentCoroutine = StartCoroutine(StopAlarm());
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
        TryStopCoroutine(_currentCoroutine);

        _alarmSource.Play();

        while (_alarmSource.volume < _maxVolume)
        {
            _alarmSource.volume += ChangeVolume();

            yield return null;
        }
    }

    private IEnumerator StopAlarm()
    {
        TryStopCoroutine(_currentCoroutine);

        while (_alarmSource.volume > _minVolume)
        {
            _alarmSource.volume -= ChangeVolume();

            yield return null;
        }

        if (_alarmSource.volume == _minVolume)
            _alarmSource.Stop();
    }
}
