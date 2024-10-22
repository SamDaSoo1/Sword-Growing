using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabBgd : MonoBehaviour
{
    Image img;
    [SerializeField] GameObject tmp1;
    [SerializeField] GameObject tmp2;
    [SerializeField] GameObject tmp3;
    [SerializeField] GameObject tmp4;
    [SerializeField] GameObject tmp5;

    void Awake()
    {
        img = GetComponent<Image>();
        Off();
    }

    public void On()
    {
        // img.enabled = true;
        tmp1.SetActive(true);
        tmp2.SetActive(true);
        tmp3.SetActive(true);
        tmp4.SetActive(true);
        tmp5.SetActive(true);
    }

    public void Off()
    {
        // img.enabled = false;
        tmp1.SetActive(false);
        tmp2.SetActive(false);
        tmp3.SetActive(false);
        tmp4.SetActive(false);
        tmp5.SetActive(false);
    }
}
