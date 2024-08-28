using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneChange : MonoBehaviour
{
    [SerializeField] RawImage maskImg1;
    [SerializeField] RawImage maskImg2;
    [SerializeField] RawImage rawImage1; // 캡처된 화면을 표시할 RawImage
    [SerializeField] RawImage rawImage2; // 캡처된 화면을 표시할 RawImage
    [SerializeField] ParticleSystem effect;
    [SerializeField] GameObject sceneChangeImg;

    [SerializeField] GameObject cctv;
    [SerializeField] RectTransform topImg;
    [SerializeField] RectTransform bottomImg;

    Texture2D screenTexture;
    bool isCoroutineRunning;

    Color alphaZero = new Color(255, 255, 255, 0);

    float movingDist = 1500.0f;
    float duration = 1.0f;

    static SceneChange instance;
    public static SceneChange Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SceneChange>();
                if(instance == null )
                {
                    GameObject go = new GameObject();
                    instance = go.AddComponent<SceneChange>();
                    go.name = "@" + typeof(SceneChange).Name;

                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        rawImage1.GetComponent<RectTransform>().sizeDelta = cctv.GetComponent<RectTransform>().rect.size;
        rawImage2.GetComponent<RectTransform>().sizeDelta = cctv.GetComponent<RectTransform>().rect.size;

        maskImg1.gameObject.SetActive(false);
        maskImg1.color = alphaZero;
        maskImg1.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        maskImg2.gameObject.SetActive(false);
        maskImg2.color = alphaZero;
        maskImg2.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        rawImage1.color = alphaZero;

        rawImage2.color = alphaZero;

        effect.Stop();
    }

    public void SceneLoad(string sceneName)
    {
        if(isCoroutineRunning)
            return;

        Init();
        StartCoroutine(CaptureRenderTexture(sceneName));
    }

    IEnumerator CaptureRenderTexture(string sceneName)
    {
        isCoroutineRunning = true;

        yield return new WaitForSeconds(0.2f);

        cctv.SetActive(true);

        yield return new WaitForSeconds(1);

        SoundManager.Instance.PlaySFX(Sfx.Effect);
        effect.Play();

        yield return new WaitForSeconds(1);

        topImg.DOAnchorPosY(topImg.anchoredPosition.y + movingDist, duration).SetEase(Ease.InQuad);
        bottomImg.DOAnchorPosY(bottomImg.anchoredPosition.y - movingDist, duration).SetEase(Ease.InQuad);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
        Init();

        yield return new WaitForSeconds(0.2f);

        maskImg1.gameObject.SetActive(true);
        maskImg2.gameObject.SetActive(true);
        sceneChangeImg.SetActive(true);

        // 프레임의 끝까지 기다려서 화면을 캡처
        yield return new WaitForEndOfFrame();

        // 화면을 Texture2D로 캡처
        screenTexture = ScreenCapture.CaptureScreenshotAsTexture();

        // RawImage에 적용
        rawImage1.texture = screenTexture;
        rawImage2.texture = screenTexture;

        // 투명도 없애기
        maskImg1.color = Color.white;
        maskImg2.color = Color.white;
        rawImage1.color = Color.white;
        rawImage2.color = Color.white;
        sceneChangeImg.SetActive(false);

        yield return new WaitForSeconds(1);

        SoundManager.Instance.PlaySFX(Sfx.Effect);
        effect.Play();

        yield return new WaitForSeconds(1);

        RectTransform rt1 = maskImg1.gameObject.GetComponent<RectTransform>();
        RectTransform rt2 = maskImg2.gameObject.GetComponent<RectTransform>();
        rt1.DOAnchorPosY(rt1.anchoredPosition.y + movingDist, duration).SetEase(Ease.InQuad);
        rt2.DOAnchorPosY(rt2.anchoredPosition.y - movingDist, duration).SetEase(Ease.InQuad);

        yield return new WaitForSeconds(1);
        Init();
        Init2();

        isCoroutineRunning = false;
    }

    void Init2()
    {
        cctv.SetActive(false);
        topImg.anchoredPosition = Vector2.zero;
        bottomImg.anchoredPosition = Vector2.zero;
        sceneChangeImg.SetActive(true);
    }
}
