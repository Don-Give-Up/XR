using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class BEStocksRecordPost : MonoBehaviour
{
    //BEStocksRecordPost
    public int stockId; // 클릭시
    public int amount; // 클릭시
    public string type; // 클릭시
    private async void Start()
    {
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);

        SendTradeRequest();
    }

    public void SendTradeRequest()
    {
        var urlData = GoogleSheetManager.Instance.UrldataGet("주식거래내역사고팔고");

        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("주식거래내역사고팔고 URL의 서버 주소가 비어있습니다.");
            return;
        }

        // 보낼 데이터를 생성
        var tradeRequest = new StockRecord
        {
            stockId = stockId,
            gameId = GameDataManager.Instance.gameId,
            stockTradeRecordAmount = amount,
            tradeType = type
        };
        // 보낼 데이터를 생성
        /*var tradeRequest = new StockRecord
        {
            stockId = 1,
            gameId = 6,
            stockTradeRecordAmount = 2,
            tradeType = "BUY"
        };*/

        PostTradeRequest(urlData.Server, tradeRequest).Forget();
    }

    private async UniTask PostTradeRequest(string url, StockRecord stockRecord)
    {
        // JSON 직렬화
        string jsonBody = JsonConvert.SerializeObject(stockRecord);

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
            Debug.Log("주식사고팔고 서버 응답 수신 성공: " + jsonResponse);

            // 추가적인 처리 필요 시 여기에 작성
        }
        else
        {
            Debug.LogError("서버 요청 실패: " + request.error);
        }
    }
}

