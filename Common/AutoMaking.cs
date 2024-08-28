using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AutoMaking : MonoBehaviour
{
    [SerializeField] ThrowingStarsMakeButton makeButton;
    [SerializeField] Slider coolTimeBar;
    [SerializeField] GameObject disableImg;
    [SerializeField] Button button;
    [SerializeField] Image img1;
    [SerializeField] Image img2;

    Color deactivateColor = new Color(1, 1, 1, 0.5f);

    int coolTime = 3;

    Coroutine co;

    private void Awake()
    {
        coolTimeBar.value = 0;
        disableImg.SetActive(false);
        button.interactable = false;
        coolTimeBar.gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        button.interactable = true;
        coolTimeBar.gameObject.SetActive(true);
        img1.color = Color.white;
        img2.color = Color.white;
        co = StartCoroutine(AutoMake());
    }

    public void Deactivate()
    {
        button.interactable = false;
        coolTimeBar.gameObject.SetActive(false);

        ColorBlock colorBlock = button.colors;
        colorBlock.disabledColor = deactivateColor;
        button.colors = colorBlock;
        img1.color = deactivateColor;
        img2.color = deactivateColor;
        gameObject.SetActive(false);
    }

    IEnumerator AutoMake()
    {
        float time = 0f;
        while(time < coolTime)
        {
            time += Time.deltaTime;
            coolTimeBar.value = time / 3;
            yield return null;
        }

        coolTimeBar.value = 1f;
        makeButton.Click(true);
        co = StartCoroutine(AutoMake());
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        // disableImg 활성화 하고, 슬라이더 끄고
        if (!disableImg.activeSelf)
        {
            disableImg.SetActive(true);
            coolTimeBar.gameObject.SetActive(false);
            StopCoroutine(co);
        }
        else
        {
            disableImg.SetActive(false);
            coolTimeBar.gameObject.SetActive(true);
            co = StartCoroutine(AutoMake());
        }
    }
}
