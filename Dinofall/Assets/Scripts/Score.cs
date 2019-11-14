using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    // static reference to our class
    private static Score _instance;
    [SerializeField] private float _tempTime;
    TextMeshProUGUI text;

    // score values
    [SerializeField] private int _amount;
    [SerializeField] private int _currentScore = 0;

    public static Score Instance    
    {
        get
        {
            return _instance;
        }

        set
        {
            // prevent assignment if Instance already exists
            if (_instance == null)
            {
                _instance = value;
            }
        }
    }

    // current score and property making it visible to other classes
    public int CurrentScore => _currentScore;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        // assign this instance if Instance == null
        if(Instance != null)
        {
            Debug.LogWarning("Copy of Score Singleton Created. Very Bad!!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // add to the current score
    public void AddScore(int amount)
    {
        // add score every 20 seconds
        if (_tempTime >= 5) 
        {
            _currentScore += amount;
            _tempTime = 0;
        }
    }

    private void Update()
    {
        _tempTime += Time.deltaTime;
        AddScore(_amount);
    }
}
