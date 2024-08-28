using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    TextMeshProUGUI messageLabel;

    void Awake()
    {
        messageLabel = GetComponent<TextMeshProUGUI>();
    }

    public void SetMessage(string username, string message)
    {
        messageLabel.text = $"{username}: {message}";
    }
}
