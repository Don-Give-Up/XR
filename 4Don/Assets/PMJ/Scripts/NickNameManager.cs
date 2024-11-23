using System;
using UnityEngine;

public class NickNameManager : MonoBehaviour
{
    public static NickNameManager Instance { get; private set; }
    
    public string Nickname { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetNickName(string newNickName)
    {
        Nickname = newNickName;
    }

    public string GetNickName()
    {
        return Nickname;
    }
}
