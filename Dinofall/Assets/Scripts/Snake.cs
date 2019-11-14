using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using DG.Tweening;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
    [SerializeField] Transform _EndPosRef;
    [SerializeField] Transform _StartPosRef;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private bool _useLocalSpace = false;
    [SerializeField] private float _speed = 0f;
    [SerializeField] private int _loops = 0;
    [SerializeField] private float _damage = 1.0f;

    [SerializeField] private SpriteRenderer _SpriteRenderer;
    public UnityEvent PositionReachedEvent;
    private float startTime;
    private float journeyLength;

    private bool _journeyRight = false;

    private void Start()
    {

        _endPosition = _EndPosRef.position;
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(transform.position, _endPosition);
    }

    private void Update()
    {
        // snake movement
        if (_journeyRight == false)
        {
            _endPosition = _EndPosRef.position;
        }
        else if (_journeyRight == true)
        {
            _endPosition = _StartPosRef.position;
        }

        //Taken from Unity API, https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        // thank you Braden
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * _speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(transform.position, _endPosition, fracJourney);

        if (Vector3.Distance(transform.position, _endPosition) < 0.2)
        {
            if (_journeyRight)
            {
                _journeyRight = false;
                startTime = Time.time;
                _SpriteRenderer.flipX = false;
            }
            else
            {
                _journeyRight = true;
                startTime = Time.time;
                _SpriteRenderer.flipX = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        // draw the line to the destination
        Vector3 destination = _useLocalSpace ? transform.position + _endPosition : _endPosition;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, destination);
    }
}

