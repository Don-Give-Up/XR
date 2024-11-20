using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class BEGameMembers : MonoBehaviour
{
    
    public async void GameMembersStart()
    {
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);

        SendGameIdRequest();
    }

    public void SendGameIdRequest()
    {
        var urlData = GoogleSheetManager.Instance.UrldataGet("게임멤버아이디");

        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("게임멤버아이디 URL의 서버 주소가 비어있습니다.");
            return;
        }

        // 보낼 데이터를 생성
        var gameMemberId = new GameMemberId
        {
            gameId = GameDataManager.Instance.gameId
        };
        
        PostChoiceProductRequest(urlData.Server, gameMemberId).Forget();
    }

    private async UniTask PostChoiceProductRequest(string url, GameMemberId gameMemberId)
    {
        // JSON 직렬화
        string jsonBody = JsonConvert.SerializeObject(gameMemberId);

        using var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", LoginCommunicator.Value);
            //"Bearer eyJkYXRlIjoxNzMxMTM4NzQxOTcwLCJ0eXBlIjoiand0IiwiYWxnIjoiSFMyNTYifQ.eyJzdWIiOiJ0b2tlbiA6IDYiLCJtZW1iZXJTY2hvb2wiOiLsi6DssL3spJEiLCJtZW1iZXJHcmFkZSI6MSwibWVtYmVyTmFtZSI6IuyGoe2YuOynhCIsIm1lbWJlck5pY2tuYW1lIjoi7Iah7Zi47KeEIiwiZXhwIjoxNzYyNjc0NzQxLCJtZW1iZXJSb2xlIjoiVEVBQ0hFUiIsIm1lbWJlckNsYXNzIjoyLCJtZW1pZXJJZCI6NiwibWVtYmVyRW1haWwiOiIwOTE4c3lqQGcuY29tIn0.pH30ziFfTxYDLMqSAfBvKUvfIdEXlRCexcO6zg5ig8k");

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("게임 멤버 아이디서버 응답 수신 성공: " + jsonResponse);

            // JSON 데이터에서 gameMemberId만 추출
            var gameMemberIdPost = JsonConvert.DeserializeObject<GameMemberIdPost>(jsonResponse);
            GameDataManager.Instance.gameMemberId = gameMemberIdPost.gameMemberId;
            
            Debug.Log("gameMemberId 값: " + GameDataManager.Instance.gameMemberId);

            // 필요한 추가 처리
        }
        else
        {
            Debug.LogError("서버 요청 실패: " + request.error);
        }
    }

}

