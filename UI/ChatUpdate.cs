using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatUpdate : MonoBehaviour
{
    Message messagePrefab;
    Transform messageContentTrans;

    void Awake()
    {
        messagePrefab = Resources.Load<Message>("Prefabs/Message");
    }

    void Start()
    {
        messageContentTrans = GameObject.Find("Canvas2").transform.Find("ChatPanel").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content");
    }

    public void AddChatMessage(string username, string message)
    {
        Message msg = Instantiate(messagePrefab, messageContentTrans);
        msg.SetMessage(username, message);
    }
}
