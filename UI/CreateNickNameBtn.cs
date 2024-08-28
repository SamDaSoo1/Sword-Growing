using TMPro;
using UnityEngine;

public class CreateNickNameBtn : MonoBehaviour
{
    TextMeshProUGUI nickName;
    GameObject loginPanel;
    FirebaseController firebaseController;

    readonly int minLength = 2;
    readonly int maxLength = 8;

    void Start()
    {
        loginPanel = GameObject.Find("Canvas").transform.Find("Login Panel").gameObject;
        nickName = loginPanel.transform.Find("InputField (TMP)").transform.Find("Text Area").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        firebaseController = FindObjectOfType<FirebaseController>();
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        int length = nickName.text.Length;
        if (length - 1 < minLength || maxLength < length - 1)
        {
            return;
        }

        PlayerPrefs.SetString("NickName", nickName.text);
        DataManager.Instance.NickName = nickName.text;
        loginPanel.SetActive(false);
        firebaseController.SignIn();
    }
}
