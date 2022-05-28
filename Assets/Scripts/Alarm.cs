using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _speedOfChanging;
    [SerializeField] private UnityEvent _playedSound;
    [SerializeField] private UnityEvent _stopedSound;

    private AudioSource _alarmSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private bool _isEntred = false;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _alarmSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            _isEntred = true;

            if(_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(PlayAlarm());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isEntred = false;
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(StopAlarm());
        }
    }

    private float ChangeVolume()
    {
        return Mathf.MoveTowards(_minVolume, _maxVolume, _speedOfChanging * Time.deltaTime);              
    }

    IEnumerator PlayAlarm()
    {
        _playedSound?.Invoke();

        while(_isEntred)
        {
            if(_alarmSource.volume < _maxVolume)
            _alarmSource.volume += ChangeVolume();

            yield return null;
        }
    }

    IEnumerator StopAlarm()
    {
        while(_alarmSource.volume > 0f)
        {
            _alarmSource.volume -= ChangeVolume();

            yield return null;
        }

        if (_isEntred == false)
            _stopedSound?.Invoke();
    }
}
