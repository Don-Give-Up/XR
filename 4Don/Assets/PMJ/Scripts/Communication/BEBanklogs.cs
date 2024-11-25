using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class BEBanklogs : MonoBehaviour
{
    public int id; // 주식 클릭하고 사기 눌렀을떄 불러주기
    public int amount; // 클릭시
    private async void Start()
    {
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);

        SendBanklogsRequest();
    }

    public void SendBanklogsRequest()
    {
        var urlData = GoogleSheetManager.Instance.UrldataGet("저축이용내역");

        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("저축이용내역 URL의 서버 주소가 비어있습니다.");
            return;
        }

       
        
        /*// 보낼 데이터를 생성
        var banklog = new Banklogs
        {
            gameId = 1,
            savingProductId = 1,
            bankTotalPrice = 2
        };*/
// 보낼 데이터를 생성
        var banklog = new Banklogs
        {
            gameId = GameDataManager.Instance.gameId,
            savingProductId = 1,
            bankTotalPrice = 2
        };
        

        PostChoiceProductRequest(urlData.Server, banklog).Forget();
    }

    private async UniTask PostChoiceProductRequest(string url, Banklogs banklogs)
    {
        // JSON 직렬화
        string jsonBody = JsonConvert.SerializeObject(banklogs);

        using var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", LoginCommunicator.Value);
        //request.SetRequestHeader("Authorization",
            //"Bearer eyJkYXRlIjoxNzMxMTM4NzQxOTcwLCJ0eXBlIjoiand0IiwiYWxnIjoiSFMyNTYifQ.eyJzdWIiOiJ0b2tlbiA6IDYiLCJtZW1iZXJTY2hvb2wiOiLsi6DssL3spJEiLCJtZW1iZXJHcmFkZSI6MSwibWVtYmVyTmFtZSI6IuyGoe2YuOynhCIsIm1lbWJlck5pY2tuYW1lIjoi7Iah7Zi47KeEIiwiZXhwIjoxNzYyNjc0NzQxLCJtZW1iZXJSb2xlIjoiVEVBQ0hFUiIsIm1lbWJlckNsYXNzIjoyLCJtZW1iZXJJZCI6NiwibWVtYmVyRW1haWwiOiIwOTE4c3lqQGcuY29tIn0.pH30ziFfTxYDLMqSAfBvKUvfIdEXlRCexcO6zg5ig8k");
            

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("저축 이용 내역 서버 응답 수신 성공: " + jsonResponse);

            // 추가적인 처리 필요 시 여기에 작성
        }
        else
        {
            Debug.LogError("서버 요청 실패: " + request.error);
        }
    }
}


