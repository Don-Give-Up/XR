using System;
using TMPro;
using UnityEngine;

public class BankClerkConversation : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text contentsText;

    public static BankClerkConversation Instance; // 인스턴스 안 만들고 해야 하는 뎅 ㅋㅋ

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Set(string name, string content)
    {
        nameText.text = $"{name}";
        contentsText.text = $"{content}";
    }
    
}
