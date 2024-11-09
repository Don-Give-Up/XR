using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 방 들어가기
/// URL 받아와서
/// 헤더에 토큰추가해서 보내고
/// 받는 값을 리스트에 저장해서
/// 그대로 UI에 띄워주기
/// </summary>
public class RoomList : MonoBehaviour
{
    public TMP_Text[] gameName;

    private List<string> gameNameList = new List<string>();


    public void GetRoomList()
    {
        var urlData = GoogleSheetManager.Instance.UrldataGet("방리스트");

        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("방리스트 URL의 서버 주소가 비어있습니다.");
            return;
        }

        GetRoomListFromUrl(urlData.Server).Forget();

    }

    private async UniTask GetRoomListFromUrl(string url)
    {
        using var request = UnityWebRequest.Get(url);

        //request.SetRequestHeader("Authorization",LoginCommunicator.Value); main
        request.SetRequestHeader("Authorization",
            "Bearer eyJkYXRlIjoxNzMxMTM4NzQxOTcwLCJ0eXBlIjoiand0IiwiYWxnIjoiSFMyNTYifQ.eyJzdWIiOiJ0b2tlbiA6IDYiLCJtZW1iZXJTY2hvb2wiOiLsi6DssL3spJEiLCJtZW1iZXJHcmFkZSI6MSwibWVtYmVyTmFtZSI6IuyGoe2YuOynhCIsIm1lbWJlck5pY2tuYW1lIjoi7Iah7Zi47KeEIiwiZXhwIjoxNzYyNjc0NzQxLCJtZW1iZXJSb2xlIjoiVEVBQ0hFUiIsIm1lbWJlckNsYXNzIjoyLCJtZW1iZXJJZCI6NiwibWVtYmVyRW1haWwiOiIwOTE4c3lqQGcuY29tIn0.pH30ziFfTxYDLMqSAfBvKUvfIdEXlRCexcO6zg5ig8k"); // test
        await request.SendWebRequest();
        
        // 요청 결과 확인
        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("서버 응답 수신 성공: " + jsonResponse);

            // 필요시 서버 응답 데이터를 추가로 처리
        }
        else
        {
            Debug.LogError("서버 요청 실패: " + request.error);
        }
        
    }
    //유저 따로 빼놓고 써야함. 여기서 배열에 차곡차곡 넣어주면 됨!!

}
