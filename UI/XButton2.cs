using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XButton2 : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        optionPanel.SetActive(!optionPanel.activeSelf);
    }
}
