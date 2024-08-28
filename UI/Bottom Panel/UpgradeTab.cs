using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTab : MonoBehaviour
{
    BottomPanel bottomPanel;
    Image btnImg;

    Color pressed = new Color(125 / 255f, 125 / 255f, 125 / 255f);
    Color normal = Color.white;

    bool isPressed = false;

    void Awake()
    {
        btnImg = transform.Find("Button").GetComponent<Image>();
    }

    void Start()
    {
        bottomPanel = GameObject.Find("Canvas").transform.Find("Bottom Panel").GetComponent<BottomPanel>();
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        bottomPanel.UpgradeTabClick(isPressed);
    }

    public void Pressed()
    {
        isPressed = true;
        btnImg.color = pressed;
    }

    public void Normal()
    {
        isPressed = false;
        btnImg.color = normal;
    }
}
