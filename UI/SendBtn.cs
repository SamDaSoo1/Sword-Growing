using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SendBtn : MonoBehaviour
{
    [SerializeField]
    TMP_InputField message;
    FirebaseController firebaseController;

    void Start()
    {
        message = GameObject.Find("Canvas2").transform.Find("ChatPanel").transform.Find("MessageBox").GetComponent<TMP_InputField>();
        firebaseController = FindObjectOfType<FirebaseController>();
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (message.text == "") return;
        string username = PlayerPrefs.GetString("NickName");
        string msg = message.text;
        firebaseController.SendChatMessage(username, msg);
        message.text = "";
    }
}
