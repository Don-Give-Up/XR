using System;
using UnityEngine;

public class RoomDataManager : MonoBehaviour
{
    public static RoomDataManager Instance { get; private set; }
    public int gameId;
    public int gameMemberId;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }
    }
}
