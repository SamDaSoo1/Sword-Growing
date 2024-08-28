using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    [SerializeField] Image bgmButton;
    [SerializeField] Sprite onBgmIcon;
    [SerializeField] Sprite offBgmIcon;
    [SerializeField] Image sfxButton;
    [SerializeField] Sprite onSfxIcon;
    [SerializeField] Sprite offSfxIcon;

    void Start()
    {
        if(PlayerPrefs.HasKey("BGM_Mute"))
            bgmButton.sprite = (PlayerPrefs.GetInt("BGM_Mute") == 1) ? offBgmIcon : onBgmIcon;
        if (PlayerPrefs.HasKey("SFX_Mute"))
            sfxButton.sprite = (PlayerPrefs.GetInt("SFX_Mute") == 1) ? offSfxIcon : onSfxIcon;
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void OnBgmClick()
    {
        SoundManager.Instance.ToggleBGMSound();
        SoundManager.Instance.PlaySFX(Sfx.Button);
        bgmButton.sprite = (bgmButton.sprite == onBgmIcon) ? offBgmIcon : onBgmIcon;
    }

    public void OnSfxClick()
    {
        SoundManager.Instance.ToggleSFXSound();
        SoundManager.Instance.PlaySFX(Sfx.Button);
        sfxButton.sprite = (sfxButton.sprite == onSfxIcon) ? offSfxIcon : onSfxIcon;
    }
}
