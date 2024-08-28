using System.Collections;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    GameObject loginPanel;
    Title title;

    void Start()
    {
        loginPanel = GameObject.Find("Canvas").transform.Find("Login Panel").gameObject;
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
