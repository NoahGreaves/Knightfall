using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private string _prefix;    // string added before text
    [SerializeField] private string _postfix;   // string added after text

    private TextMeshProUGUI _text;      // the attached text component

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // append our prefix, score, and postfix together and assign to text box
        _text.text = _prefix + Score.Instance.CurrentScore.ToString() + _postfix;
    }
}
