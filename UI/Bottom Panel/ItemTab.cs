using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTab : MonoBehaviour
{
    [SerializeField] BottomPanel bottomPanel;
    [SerializeField] Image btnImg;

    Color pressed = new Color(125 / 255f, 125 / 255f, 125 / 255f);
    Color normal = Color.white;

    bool isPressed = false;

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        bottomPanel.ItemTabClick(isPressed);
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
