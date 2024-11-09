using TMPro;
using UnityEngine;

public class Room : MonoBehaviour
{
    public TMP_Text gameName;

    public void UseData(RoomData roomData)
    {
        gameName.text = roomData.gameName;
    }
}
