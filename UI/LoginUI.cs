using System.Collections;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    [SerializeField] GameObject loginPanel;
    Title title;

    void Start()
    {
        loginPanel.SetActive(false);
        title = FindObjectOfType<Title>();
    }

    public void OnEnableLoginPanel()
    {
        loginPanel.SetActive(true);
    }

    public void OnEnableGameStartBtn()
    {
        StartCoroutine(title.CoActions());
    }
}
