using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class AIPostNewsHintSmall : MonoBehaviour
{
    public NewsHintsmall[] news1;
    private int round;
    
    private void Start()
    {
        RoundSystem.Instance.onRoundChange += Day;
        StartCoroutine(Test());
        
    }

    private void Day(int round)
    {
        this.round = round;
    }
    private IEnumerator Test()
    {
        if (!GoogleSheetManager.Instance.IsLoaded)
            yield return new WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);

        var urlData = GoogleSheetManager.Instance.UrldataGet("힌트뉴스데이터");


        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("힌트뉴스데이터 URL의 서버 주소가 비어있습니다.");
            yield break;
        }
        else
        {
            Debug.Log("힌트뉴스데이터 url 받아짐");
        }

        Debug.Log(urlData.Name);
        Debug.Log(urlData.Server);
        StartCoroutine(PostAndFetchArticlesData(urlData.Server));
    }

    // 데이터를 POST로 보내고 응답을 받아오는 코루틴
    IEnumerator PostAndFetchArticlesData(string url)
    {
        // 서버에 보낼 데이터
        var requestData = new Dictionary<string, int>
        {
            { "year", this.round }
        };

        Debug.Log("힌트 뉴스 요청");
        // 데이터를 JSON으로 직렬화
        string jsonRequestData = JsonConvert.SerializeObject(requestData);

        // POST 요청 생성
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        // JSON 데이터를 바이트로 변환하여 업로드 핸들러에 설정
        byte[] jsonToSend = System.Text.Encoding.UTF8.GetBytes(jsonRequestData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Content-Type 헤더 설정 (JSON 데이터를 전송하므로)
        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("힌트 뉴스 기다리는 중");
        // 서버에 요청을 보내고 응답을 기다림
        yield return request.SendWebRequest();

        Debug.Log("힌트 뉴스 받음");
        // 요청이 성공했는지 확인
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 성공 시 서버로부터 받은 응답 처리
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Response: " + jsonResponse);

            // JSON 응답을 ArticlesData 객체로 역직렬화
            CompanyData companyData = JsonConvert.DeserializeObject<CompanyData>(jsonResponse);
            UseArticlesData(companyData);
        }
        else
        {
            // 요청 실패 시 오류 메시지 출력
            Debug.LogError("Error: " + request.error);
        }
    }


    // 각 Article의 데이터를 개별적으로 사용
    private void UseArticlesData(CompanyData companyData)
    {
        news1[0].UseData(companyData.minjeong);
        news1[1].UseData(companyData.hojin);
        news1[2].UseData(companyData.meta);
        news1[3].UseData(companyData.boyeong);
        news1[4].UseData(companyData.yeowon);
        news1[5].UseData(companyData.yujin);
        news1[6].UseData(companyData.chaeho);
        news1[7].UseData(companyData.minju);
        
    }

}
