using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BgmSound
{
    MainBGM,
    MinigameBGM,
    BossBGM1,
    BossBGM2
}

public enum Sfx
{
    Button,
    Effect,
    Hit1,
    Hit2,
    Obstacle,
    PurchaseFailed,
    PurchaseSuccessed,
    SwordCreate,
    Throwing
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> BgmList;
    [SerializeField] List<AudioClip> SfxList;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;

    [SerializeField] List<AudioSource> sfxPool;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        sfxPool = new List<AudioSource>();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("BGM_Mute"))
            bgm.mute = (PlayerPrefs.GetInt("BGM_Mute") == 1) ? true : false;
        if (PlayerPrefs.HasKey("SFX_Mute"))
            sfx.mute = (PlayerPrefs.GetInt("SFX_Mute") == 1) ? true : false;
    }

    AudioSource GetSFX()
    {
        AudioSource select = null;

        foreach (AudioSource audioSource in sfxPool)
        {
            if (audioSource != null && audioSource.isPlaying == false)
            {
                select = audioSource;
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(sfx, transform);
            sfxPool.Add(select);
        }

        return select;
    }

    public void PlayBGM(BgmSound type)
    {
        bgm.clip = BgmList[(int)type];
        bgm.Play();
    }

    public float BGMLength(BgmSound type)
    {
        return BgmList[(int)type].length;
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void PlaySFX(Sfx type, float time = 0f)
    {
        AudioSource sfx = GetSFX();
        sfx.clip = SfxList[(int)type];
        sfx.time = time;
        sfx.Play();
    }

    public void AllStopSFX()
    {
        foreach (AudioSource sfx in sfxPool)
        {
            sfx.Stop();
        }
    }

    public void ToggleBGMSound()
    {
        bgm.mute = !bgm.mute;

        if(bgm.mute)
            PlayerPrefs.SetInt("BGM_Mute", 1);
        else
            PlayerPrefs.SetInt("BGM_Mute", 0);
    }

    public void ToggleSFXSound()
    {
        sfx.mute = !sfx.mute;
        foreach (AudioSource sfx in sfxPool)
        {
            sfx.mute = !sfx.mute;
        }

        if (sfx.mute)
            PlayerPrefs.SetInt("SFX_Mute", 1);
        else
            PlayerPrefs.SetInt("SFX_Mute", 0);
    }

    public void BGMSoundPitchUp()
    {
        bgm.pitch += 0.025f;
    }

    public void BGMSoundPitchReset()
    {
        bgm.pitch = 1f;
    }
}