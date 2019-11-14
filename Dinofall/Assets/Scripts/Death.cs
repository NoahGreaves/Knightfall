using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    [SerializeField] private string _death = "Death";
    [SerializeField] private string _screenDeath = "ScreenDeath";
    [SerializeField] private string _ground = "Ground";
    [SerializeField] private UnityEvent _event;
    protected Animator Animator {get; private set;}

    private void Start()
    {
        // gets the animator at the start of the game
        Animator = GetComponent<Animator>();
    }

    // kills the player if the touch the 'death' objects at the top and bottom of the screen
    private void OnCollisionEnter2D(Collision2D other)
    {
        // check if the player collided with an enemy
        if (other.gameObject.CompareTag(_death)) 
        {
            Animator.SetBool("Death", true);    // plays the death animation
            _event?.Invoke();                   // enable the game over screen
        }
    }
}
