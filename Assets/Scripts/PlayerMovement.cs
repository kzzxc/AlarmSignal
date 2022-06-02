using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Move(_speed);
            _sprite.flipX = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(-_speed);
            _sprite.flipX = true;
        }       
    }

    private void Move(float speed)
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }    
}
