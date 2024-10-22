using TMPro;
using UnityEngine;

public class CreateNickNameBtn : MonoBehaviour
{
    [SerializeField] GameObject loginPanel;
    [SerializeField] TextMeshProUGUI nickName;
    [SerializeField] FirebaseController firebaseController;

    readonly int minLength = 2;
    readonly int maxLength = 8;

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        int length = nickName.text.Length;
        if (length - 1 < minLength || maxLength < length - 1)
        {
            return;
        }

        PlayerPrefs.SetString("NickName", nickName.text);
        loginPanel.SetActive(false);
        firebaseController.SignIn();
    }
}
