using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatUpdate : MonoBehaviour
{
    Message messagePrefab;
    [SerializeField] Transform messageContentTrans;

    void Awake()
    {
        messagePrefab = Resources.Load<Message>("Prefabs/Message");
    }

    public void AddChatMessage(string username, string message)
    {
        Message msg = Instantiate(messagePrefab, messageContentTrans);
        msg.SetMessage(username, message);
    }
}
