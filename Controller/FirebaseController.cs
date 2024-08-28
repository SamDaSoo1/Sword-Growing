using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Firebase.Database;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FirebaseController : MonoBehaviour
{
    LoginUI loginUI;
    FirebaseAuth auth;
    FirebaseUser user;
    ChatUpdate chatUpdate;
    DatabaseReference chatDB;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnChangedScene;
    }

    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync()
                            .ContinueWithOnMainThread(task =>
                            {
                                if(task.Result == Firebase.DependencyStatus.Available)
                                {
                                    FirebaseInit();
                                }
                                else
                                {
                                    Debug.LogError("CheckAndFixDependenciesAsync Failed");
                                }
                            });
    }

    void OnChangedScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lobby")
        {
            loginUI = FindObjectOfType<LoginUI>();
        }
        else if (scene.name == "Main")
        {
            chatUpdate = FindObjectOfType<ChatUpdate>();
        }
    }

    void FirebaseInit()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;

        // Firebase 사이트의 RTDB 루트의 하위에 있는 ChatMessage에 접근함
        chatDB = FirebaseDatabase.DefaultInstance.GetReference("ChatMessage");
        chatDB.OrderByChild("timestamp").LimitToLast(1).ValueChanged -= ReceiveMessage;
        chatDB.OrderByChild("timestamp").LimitToLast(1).ValueChanged += ReceiveMessage;
    }

    void AuthStateChanged(object sender, EventArgs e)
    {
        FirebaseAuth senderAuth = sender as FirebaseAuth;
        if(senderAuth != null)
        {
            user = senderAuth.CurrentUser;
            if(user != null) 
            {
                //Debug.Log(user.UserId);
                loginUI?.OnEnableGameStartBtn();
            }
            else
            {
                loginUI?.OnEnableLoginPanel();
            }
        }
    }

    public void SignIn()
    {
        SignInAnonymous();
    }

    Task SignInAnonymous()
    {
        return auth.SignInAnonymouslyAsync()
                   .ContinueWithOnMainThread(task =>
                   {
                       if(task.IsFaulted)
                       {
                           Debug.LogError("SignIn Failed");
                       }
                       else if(task.IsCompleted)
                       {
                           Debug.Log("SignIn Completed");
                       }
                   });
    }

    public void SignOut()
    {
        auth.SignOut();
    }

    public void ReadChatMessage()
    {
        
        chatDB.GetValueAsync()
              .ContinueWithOnMainThread(task =>
              {
                  if(task.IsFaulted)
                  {
                      Debug.LogError("ReadErr");
                  }
                  else if(task.IsCompleted)
                  {
                      DataSnapshot snapshot = task.Result;
                      Debug.Log("ChildrenCount: " + snapshot.ChildrenCount);
                      foreach(DataSnapshot message in snapshot.Children)
                      {
                          Debug.Log("키 값 : " + message.Key + 
                                    ", 보낸사람 : " + message.Child("username").Value.ToString() + 
                                    ", 내용 : " + message.Child("message").Value.ToString());
                      }
                  }
              });
    }

    public void SendChatMessage(string username, string message)
    {
        chatDB.OrderByChild("timestamp").LimitToLast(1).ValueChanged -= ReceiveMessage;
        chatDB.OrderByChild("timestamp").LimitToLast(1).ValueChanged += ReceiveMessage;

        string key = chatDB.Push().Key;        // RTDB에서 랜덤 키를 생성해서 그 값을 가져옴

        Dictionary<string, object> msgDic= new Dictionary<string, object>();
        msgDic.Add("username", username);
        msgDic.Add("message", message);
        msgDic.Add("timestamp", ServerValue.Timestamp);

        Dictionary<string, object> updateMsg = new Dictionary<string, object>();
        updateMsg.Add(key, msgDic);

        chatDB.UpdateChildrenAsync(updateMsg)
              .ContinueWithOnMainThread(task =>
              {
                  if(task.IsCompleted)
                  {
                      //Debug.Log("SendChatMessage Complete");
                  }
              });
    }

    HashSet<string> receivedKeyList = new HashSet<string>();

    public void ReceiveMessage(object sender, ValueChangedEventArgs e)
    {
        DataSnapshot snapshot = e.Snapshot;
        
        if(snapshot == default)
        {
            print("리턴");
            return;
        }
        //Debug.Log("ChildrenCount: " + snapshot.ChildrenCount);
        foreach (DataSnapshot message in snapshot.Children)
        {
            if (receivedKeyList.Contains(message.Key))
                continue;

            receivedKeyList.Add(message.Key);
            //Debug.Log("키 값 : " + message.Key +
            //          ", 보낸사람 : " + message.Child("username").Value.ToString() +
            //          ", 내용 : " + message.Child("message").Value.ToString());

            if(chatUpdate != null)
            {
                string username = message.Child("username").Value.ToString();
                string msg = message.Child("message").Value.ToString();
                chatUpdate.AddChatMessage(username, msg);
            }
        }
    }
}
