using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneChange : MonoBehaviour
{
    [SerializeField] GameObject SceneChangeImg2;
    [SerializeField] GameObject SceneChangeImg;
    [SerializeField] RectTransform topMaskImgRect;
    [SerializeField] RectTransform topRawImgRect;
    [SerializeField] RectTransform bottomMaskImgRect;
    [SerializeField] RectTransform bottomRawImgRect;
    [SerializeField] GameObject topMaskImg;
    [SerializeField] RawImage topRawImg;
    [SerializeField] GameObject bottomMaskImg;
    [SerializeField] RawImage bottomRawImg;
    [SerializeField] RectTransform resolution;
    [SerializeField] Canvas canvas;
    ParticleSystem effect;
    Texture2D screenTexture;
    [SerializeField] RenderTexture renderTexture;
    bool isCoroutineRunning;
    float movingDist = 2000.0f;
    float duration = 1.0f;

    static SceneChange instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        topMaskImgRect.sizeDelta = new Vector2(resolution.sizeDelta.x, resolution.sizeDelta.y);
        topRawImgRect.sizeDelta = new Vector2(resolution.sizeDelta.x, resolution.sizeDelta.y);
        bottomMaskImgRect.sizeDelta = new Vector2(resolution.sizeDelta.x, resolution.sizeDelta.y);
        bottomRawImgRect.sizeDelta = new Vector2(resolution.sizeDelta.x, resolution.sizeDelta.y);

        SceneChangeImg2.SetActive(false);
        SceneChangeImg.SetActive(false);
        topMaskImg.SetActive(false);
        bottomMaskImg.SetActive(false);
        renderTexture = Resources.Load<RenderTexture>("RenderTexture");
        topRawImg.texture = renderTexture;
        bottomRawImg.texture = renderTexture;
        effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effect").GetComponent<ParticleSystem>(), transform);
        var mainModule = effect.main;
        mainModule.startRotation = 28.71f * (canvas.GetComponent<RectTransform>().sizeDelta.y / 1920.0f ) * Mathf.Deg2Rad;
        effect.gameObject.name = "SceneChange.effect";
        effect.Stop();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (canvas == null)
        {
            canvas = transform.GetChild(0).GetComponent<Canvas>();
        }
        canvas.worldCamera = Camera.main;

        if(scene.name == "Main")
        {
            resolution = GameObject.Find("Canvas8(Resolution)").GetComponent<RectTransform>();
        }
    }

    public void SceneLoad(string sceneName)
    {
        if(isCoroutineRunning)
            return;

        StartCoroutine(CaptureRenderTexture(sceneName));
    }

    IEnumerator CaptureRenderTexture(string sceneName)
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.2f);

        SoundManager.Instance.PlaySFX(Sfx.Effect);
        effect.Play();
        yield return new WaitForEndOfFrame();

        screenTexture = ScreenCapture.CaptureScreenshotAsTexture();
        topRawImg.texture = screenTexture;
        bottomRawImg.texture = screenTexture;
        topMaskImg.SetActive(true);
        bottomMaskImg.SetActive(true);
        SceneChangeImg2.SetActive(true);
        SceneChangeImg.SetActive(true);
        yield return new WaitForSeconds(1);

        topMaskImgRect.DOAnchorPosY(topMaskImgRect.anchoredPosition.y + movingDist, duration).SetEase(Ease.InQuad);
        bottomMaskImgRect.DOAnchorPosY(bottomMaskImgRect.anchoredPosition.y - movingDist, duration).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitForSeconds(1);

        InitSetting();
        SoundManager.Instance.PlaySFX(Sfx.Effect);
        effect.Play();
        yield return new WaitForEndOfFrame();

        screenTexture = ScreenCapture.CaptureScreenshotAsTexture();
        topRawImg.texture = screenTexture;
        bottomRawImg.texture = screenTexture;
        topMaskImg.SetActive(true);
        bottomMaskImg.SetActive(true);
        SceneChangeImg2.SetActive(false);
        SceneChangeImg.SetActive(false);
        yield return new WaitForSeconds(1);

        topMaskImgRect.DOAnchorPosY(topMaskImgRect.anchoredPosition.y + movingDist, duration).SetEase(Ease.InQuad);
        bottomMaskImgRect.DOAnchorPosY(bottomMaskImgRect.anchoredPosition.y - movingDist, duration).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(1);

        InitSetting();
        isCoroutineRunning = false;
    }

    void InitSetting()
    {
        topMaskImg.SetActive(false);
        bottomMaskImg.SetActive(false);
        topMaskImgRect.anchoredPosition = Vector2.zero;
        bottomMaskImgRect.anchoredPosition = Vector2.zero;
        topRawImg.texture = renderTexture;
        bottomRawImg.texture = renderTexture;
    }
}
