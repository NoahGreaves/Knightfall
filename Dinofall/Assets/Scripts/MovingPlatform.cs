using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifeTime = 10f;

    protected Rigidbody2D _rigidbody;

    private void Awake()
    {
        // get the rigidbody
        _rigidbody = GetComponent<Rigidbody2D>();   

        // launch the projectile
        _rigidbody.velocity = transform.up * _speed;

        // destroy the game object after lifeTime seconds
        Destroy(gameObject, _lifeTime);
    }
}
