using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public TMP_Text gameName;
    public RoomData roomData;
    public Button bt;
    public BEGameMembers beGameMembers;
    private bool _isSelect;

    private void Start()
    {
        bt = GetComponent<Button>();
        bt.onClick.AddListener(() => ToggleSelectButton(true));
        bt.onClick.AddListener(() => Debug.Log("민주 사랑해"));
    }

    public void UseData(RoomData roomData)
    {
        gameName.text = roomData.gameName;
        this.roomData = roomData;
    }

    public async void CheckPassword(string text)
    {
        if (!_isSelect)
            return;
        
        Debug.Log("아야 버튼눌렸냐?!");
        
        if (roomData.gamePassword == text)
        {
            Debug.Log("비밀번호가 옳바릅니다.");
            
            GameDataManager.Instance.gameId = roomData.gameId;
            Debug.Log("GameId : " + roomData.gameId);
            
            // SceneManager.LoadScene("Room");
            beGameMembers.GameMembersStart();
            await PhoStartGame.Instance.JoinSquare();
            
            
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다.");
        }
    }

    public void ToggleSelectButton(bool isSelect)
    {
        _isSelect = isSelect;
        Debug.Log("게임 이름 : " + gameName.text + (_isSelect ? "(이)가 선택 되었습니다." : " 선택 취소 됐습니다."));
    }
}
