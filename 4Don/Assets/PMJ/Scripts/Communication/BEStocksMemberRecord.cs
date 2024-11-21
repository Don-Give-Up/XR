using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class BEStocksMemberRecord : MonoBehaviour
{
    public StockRecordMemberGet[] stockmembers; // 데이터를 저장할 배열

    private async void Start()
    {
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);

        FetchStockRecords();
    }

    private void FetchStockRecords()
    {
        var urlData = GoogleSheetManager.Instance.UrldataGet("주식특정멤버조회");

        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("주식특정멤버조회 URL의 서버 주소가 비어있습니다.");
            return;
        }
        string finalUrl = AppendMemberIdToUrl(urlData.Server, GameDataManager.Instance.gameMemberId);
        Debug.Log("최종 URL: " + finalUrl);
        
        GetStockRecordsFromServer(finalUrl).Forget();
    }
    private string AppendMemberIdToUrl(string baseUrl, int memberId)
    {
        // URL 끝에 "/"가 없으면 추가
        if (!baseUrl.EndsWith("/"))
        {
            baseUrl += "/";
        }

        // 멤버 ID를 URL에 추가
        return baseUrl + memberId;
    }

    private async UniTask GetStockRecordsFromServer(string url)
    {
        using var request = UnityWebRequest.Get(url);

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", LoginCommunicator.Value);
        //request.SetRequestHeader("Authorization",
          // "Bearer eyJkYXRlIjoxNzMxMTM4NzQxOTcwLCJ0eXBlIjoiand0IiwiYWxnIjoiSFMyNTYifQ.eyJzdWIiOiJ0b2tlbiA6IDYiLCJtZW1iZXJTY2hvb2wiOiLsi6DssL3spJEiLCJtZW1iZXJHcmFkZSI6MSwibWVtYmVyTmFtZSI6IuyGoe2YuOynhCIsIm1lbWJlck5pY2tuYW1lIjoi7Iah7Zi47KeEIiwiZXhwIjoxNzYyNjc0NzQxLCJtZW1iZXJSb2xlIjoiVEVBQ0hFUiIsIm1lbWJlckNsYXNzIjoyLCJtZW1iZXJJZCI6NiwibWVtYmVyRW1haWwiOiIwOTE4c3lqQGcuY29tIn0.pH30ziFfTxYDLMqSAfBvKUvfIdEXlRCexcO6zg5ig8k");

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("주식 특정 멤버 조회 서버 응답 수신 성공: " + jsonResponse);

            // JSON 데이터를 List<StockRecordMemberGet>로 파싱
            List<StockRecordMemberGet> stockRecordList = JsonConvert.DeserializeObject<List<StockRecordMemberGet>>(jsonResponse);
            UseStocksData(stockRecordList);
        }
        else
        {
            Debug.LogError("서버 요청 실패: " + request.error);
        }
    }

    private void UseStocksData(List<StockRecordMemberGet> stockRecordList)
    {
        // 데이터를 StockRecordMemberGet 배열로 변환하여 저장
        stockmembers = stockRecordList.ToArray();

        // 변환된 데이터를 출력
        foreach (var stock in stockmembers)
        {
            Debug.Log($"Trade ID: {stock.stockTradeRecordId}, Member ID: {stock.gameMemberId}, " +
                      $"Amount: {stock.stockTradeRecordAmount}, Type: {stock.tradeType}, " +
                      $"Stock Name: {stock.stockName}, Total Price: {stock.stockTotalPrice}");
        }
    }
}
