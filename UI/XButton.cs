using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XButton : MonoBehaviour
{
    [SerializeField] GameObject chatPanel;

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        chatPanel.SetActive(!chatPanel.activeSelf);
    }
}
