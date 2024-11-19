using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomData
{
    public int gameId;
    public int memberId;
    public string gameName;
    public string gamePassword;
}

[System.Serializable]
public class RoomListData
{
    public List<RoomData> roomList;
}
