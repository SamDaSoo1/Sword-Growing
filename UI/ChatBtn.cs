using UnityEngine;

public class ChatBtn : MonoBehaviour
{
    GameObject chatPanel;

    void Start()
    {
        chatPanel = GameObject.Find("Canvas2").transform.Find("ChatPanel").gameObject;
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        chatPanel.SetActive(!chatPanel.activeSelf);
    }
}
