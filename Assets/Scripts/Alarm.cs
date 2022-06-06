using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        TryStopCoroutine(_currentCoroutine);

        if (isEntred)
        {
            _alarmSource.Play();
            _currentCoroutine = StartCoroutine(ChangeVolume(_maxVolume));
        }
        else
        {
            _currentCoroutine = StartCoroutine(ChangeVolume(_minVolume));
        }
    }

    private void TryStopCoroutine(Coroutine currentCoroutine)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }

    private IEnumerator ChangeVolume(float endVolume)
    {
        while (endVolume != _alarmSource.volume)
        {
            _alarmSource.volume = Mathf.MoveTowards(_alarmSource.volume, endVolume, _speedOfChanging * Time.deltaTime);
            yield return null;
        }

        if (_alarmSource.volume == _minVolume)
            _alarmSource.Stop();
    }
}
