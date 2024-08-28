using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NickName : MonoBehaviour
{
    TextMeshProUGUI tmp;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = DataManager.Instance.NickName;
    }
}
