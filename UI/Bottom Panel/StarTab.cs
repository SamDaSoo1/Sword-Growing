using UnityEngine;
using UnityEngine.UI;

public class StarTab : MonoBehaviour
{
    [SerializeField] BottomPanel bottomPanel;
    [SerializeField] Image btnImg;

    Color pressed = new Color(125 / 255f, 125 / 255f, 125 / 255f);
    Color normal = Color.white;

    bool isPressed = false;

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        bottomPanel.StarTabClick(isPressed);
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
