using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MadeRoomData : MonoBehaviour
{
    public TMP_InputField memberIdField;
    public TMP_InputField roomNameField;
    public TMP_InputField roomPasswordField;

    public PostMadeRoomData GetRoomMade()
    {
        // PostMadeRoomData의 인스턴스를 생성하여 필요한 데이터를 저장
        PostMadeRoomData req = new PostMadeRoomData()
        {
            memberId = int.Parse(memberIdField.text),
            gameName = roomNameField.text,
            gamePassword = roomPasswordField.text
        };
        return req;
    }
}

[System.Serializable]
public class PostMadeRoomData
{
    public int memberId;
    public string gameName;
    public string gamePassword;
}