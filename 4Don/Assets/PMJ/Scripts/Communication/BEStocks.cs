using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class BEStocks : MonoBehaviour
{
    public Stock[] stocks;

    private async void Start()
    {
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);

        GetStocks();
    }

    public void GetStocks()
    {
        var urlData = GoogleSheetManager.Instance.UrldataGet("주식정보전체조회");

        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("주식정보전체조회 URL의 서버 주소가 비어있습니다.");
            return;
        }

        GetStocksFromUrl(urlData.Server).Forget();
    }

    private async UniTask GetStocksFromUrl(string url)
    {
        using var request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", LoginCommunicator.Value);
        //request.SetRequestHeader("Authorization",
            //"Bearer eyJkYXRlIjoxNzMxMTM4NzQxOTcwLCJ0eXBlIjoiand0IiwiYWxnIjoiSFMyNTYifQ.eyJzdWIiOiJ0b2tlbiA6IDYiLCJtZW1iZXJTY2hvb2wiOiLsi6DssL3spJEiLCJtZW1iZXJHcmFkZSI6MSwibWVtYmVyTmFtZSI6IuyGoe2YuOynhCIsIm1lbWJlck5pY2tuYW1lIjoi7Iah7Zi47KeEIiwiZXhwIjoxNzYyNjc0NzQxLCJtZW1iZXJSb2xlIjoiVEVBQ0hFUiIsIm1lbWJlckNsYXNzIjoyLCJtZW1iZXJJZCI6NiwibWVtYmVyRW1haWwiOiIwOTE4c3lqQGcuY29tIn0.pH30ziFfTxYDLMqSAfBvKUvfIdEXlRCexcO6zg5ig8k");

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("서버 응답 수신 성공: " + jsonResponse);

            // JSON 데이터를 List<Stock>로 파싱
            List<Stock> stockList = JsonConvert.DeserializeObject<List<Stock>>(jsonResponse);
            UseStocksData(stockList);
        }
        else
        {
            Debug.LogError("서버 요청 실패: " + request.error);
        }
    }

    private void UseStocksData(List<Stock> stockList)
    {
        // 데이터를 원하는 방식으로 처리
        stocks = stockList.ToArray(); // 필요시 배열로 변환
        foreach (var stock in stocks)
        {
            Debug.Log($"Stock ID: {stock.stockId}, Name: {stock.stockName}, Price: {stock.stockPrice}");
        }
    }
}
