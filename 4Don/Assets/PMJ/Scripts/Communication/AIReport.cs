using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;

public class AIReport : MonoBehaviour
{
    private string apiUrl = "https://example.com/api"; // API URL을 여기에 입력

    private async void Start()
    {
        // GoogleSheetManager의 데이터가 로드될 때까지 기다립니다.
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);

        // AI 보고서를 가져옵니다.
        FetchAIReport();
    }

    private void FetchAIReport()
    {
        // GoogleSheetManager에서 "분석레포트"에 해당하는 URL 데이터를 가져옵니다.
        var urlData = GoogleSheetManager.Instance.UrldataGet("분석레포트");

        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("분석레포트 URL의 서버 주소가 비어있습니다.");
            return;
        }

        // 가져온 URL 데이터를 Post 요청에 사용하여 AI 보고서를 가져옵니다.
        PostAIReportFromAI(urlData.Server).Forget();
    }

    private async UniTask PostAIReportFromAI(string urlData)
    {
        // 서버로 보낼 데이터 생성 (예: id 값)
        var requestData = new { id = "1" }; // 보낼 데이터 설정
        string jsonString = JsonConvert.SerializeObject(requestData); 

        // UnityWebRequest 설정
        UnityWebRequest request = new UnityWebRequest(urlData, "POST"); 
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString); 
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer(); 
        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("오래 걸리는거임 오해 ㄴ노 보내긴함");
        // 요청 보내기
        await request.SendWebRequest();  

        // 응답 처리
        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text; 
            Debug.Log("Response: " + jsonResponse);

           
            ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(jsonResponse);
            ProcessResponse(responseData); 
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    // JSON 응답을 처리하는 메서드
    private void ProcessResponse(ResponseData responseData)
    {
        // 응답 데이터에서 player_id와 자산 정보 출력
        Debug.Log($"Player ID: {responseData.raw_Data.player_id}");
        Debug.Log($"Total Assets: {responseData.raw_Data.assets.total}");
    }
}

