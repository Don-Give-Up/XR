using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public TMP_Text gameName;
    public TMP_InputField password;
    public RoomData roomData;
    public void UseData(RoomData roomData)
    {
        gameName.text = roomData.gameName;
        this.roomData = roomData;
    }

    public async void CheckPassword(string text)
    {
        Debug.Log("아야 버튼눌렸냐?!");
        if (roomData.gamePassword == password.text)
        {
            Debug.Log("비밀번호가 옳바릅니다.");
            // SceneManager.LoadScene("Room");
            await PhoStartGame.Instance.JoinSquare();
          
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다.");
        }
    }
}
