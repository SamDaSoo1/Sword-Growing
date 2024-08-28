using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartBtn : MonoBehaviour
{
    Button btn;
    Image img;

    void Awake()
    {
        btn = GetComponent<Button>();
        img = GetComponent<Image>();
        btn.interactable = false;
        img.color = Color.clear;
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        SceneManager.LoadScene("Main");
    }

    public void Appear()
    {
        btn.interactable = true;
        img.color = Color.white;
    }
}
