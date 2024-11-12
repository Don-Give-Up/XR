using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class MadeRoom : MonoBehaviour
{
    public MadeRoomData madeRoomData;

    // 방 생성 시작
    public void RoomMade()
    {
        
        TestAsync();
    }

    // 방 생성 데이터를 서버로 보내기 전 URL 데이터 확인
    public void TestAsync()
    {
        var urlData = GoogleSheetManager.Instance.UrldataGet("방만들기");

        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("방만들기 URL의 서버 주소가 비어있습니다.");
            return;
        }

        Debug.Log("방만들기 URL 받아짐: " + urlData.Server);

        // MadeRoomData에서 입력된 데이터를 가져와 PostMadeRoomData 객체로 변환
        PostMadeRoomData requestData = madeRoomData.GetRoomMade();
        Debug.Log($"Room Name: {requestData.gameName}, Room Password: {requestData.gamePassword}");

        // 비동기 POST 요청 실행
        RoomMadePostAsync(urlData.Server, requestData).Forget();
    }

    // 서버에 방 생성 요청을 보내는 비동기 메서드
    public async UniTask RoomMadePostAsync(string url, PostMadeRoomData requestData)
    {
        
        
        // 데이터를 JSON으로 직렬화
        string jsonRequestData = JsonConvert.SerializeObject(requestData);

        using var request = UnityWebRequest.Post(url, jsonRequestData, "application/json");
        
        Debug.Log("서버로 POST 요청 보내는 중...");
        //request.SetRequestHeader("Authorization", LoginCommunicator.Value);
        request.SetRequestHeader("Authorization", "Bearer eyJkYXRlIjoxNzMxMTM4NzQxOTcwLCJ0eXBlIjoiand0IiwiYWxnIjoiSFMyNTYifQ.eyJzdWIiOiJ0b2tlbiA6IDYiLCJtZW1iZXJTY2hvb2wiOiLsi6DssL3spJEiLCJtZW1iZXJHcmFkZSI6MSwibWVtYmVyTmFtZSI6IuyGoe2YuOynhCIsIm1lbWJlck5pY2tuYW1lIjoi7Iah7Zi47KeEIiwiZXhwIjoxNzYyNjc0NzQxLCJtZW1iZXJSb2xlIjoiVEVBQ0hFUiIsIm1lbWJlckNsYXNzIjoyLCJtZW1iZXJJZCI6NiwibWVtYmVyRW1haWwiOiIwOTE4c3lqQGcuY29tIn0.pH30ziFfTxYDLMqSAfBvKUvfIdEXlRCexcO6zg5ig8k"); // test
        // 비동기 요청을 보내고 응답 대기
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
}
