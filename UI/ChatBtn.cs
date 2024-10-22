using UnityEngine;

public class ChatBtn : MonoBehaviour
{
    [SerializeField] GameObject chatPanel;

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        chatPanel.SetActive(!chatPanel.activeSelf);
    }
}
