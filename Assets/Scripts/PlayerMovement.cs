using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private const string _isRunning = "isRunning";

    private SpriteRenderer _sprite;
    private Animator _animator;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            _animator.SetBool(_isRunning, true);
            Move(_speed);
            _sprite.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            _animator.SetBool(_isRunning, true);
            Move(-_speed);
            _sprite.flipX = true;
        }
        else
            _animator.SetBool(_isRunning, false);
    }

    private void Move(float speed)
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
