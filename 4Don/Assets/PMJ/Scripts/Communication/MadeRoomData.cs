using TMPro;
using UnityEngine;

public class MadeRoomData : MonoBehaviour
{
    public TMP_InputField roomNameField;
    public TMP_InputField roomPasswordField;

    public PostMadeRoomData GetRoomMade()
    {
        // PostMadeRoomData의 인스턴스를 생성하여 필요한 데이터를 저장
        PostMadeRoomData req = new PostMadeRoomData()
        {
            roomName = roomNameField.text,
            roomPassword = roomPasswordField.text
        };
        return req;
    }
}

[System.Serializable]
public class PostMadeRoomData
{
    public string roomName;
    public string roomPassword;
}