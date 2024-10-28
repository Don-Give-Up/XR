using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class AIAritclePost : MonoBehaviour
{
    string postUrl = "https://f45d-221-163-19-142.ngrok-free.app/news"; // 실제 API URL로 변경

    void Start()
    {
        StartCoroutine(PostAndFetchArticlesData());
    }

    // 데이터를 POST로 보내고 응답을 받아오는 코루틴
    IEnumerator PostAndFetchArticlesData()
    {
        // 서버에 보낼 데이터
        var requestData = new Dictionary<string, string>
        {
            { "quarter", "199603" }
        };

        // 데이터를 JSON으로 직렬화
        string jsonRequestData = JsonConvert.SerializeObject(requestData);

        // POST 요청 생성
        UnityWebRequest request = new UnityWebRequest(postUrl, "POST");

        // JSON 데이터를 바이트로 변환하여 업로드 핸들러에 설정
        byte[] jsonToSend = System.Text.Encoding.UTF8.GetBytes(jsonRequestData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Content-Type 헤더 설정 (JSON 데이터를 전송하므로)
        request.SetRequestHeader("Content-Type", "application/json");

        // 서버에 요청을 보내고 응답을 기다림
        yield return request.SendWebRequest();

        // 요청이 성공했는지 확인
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 성공 시 서버로부터 받은 응답 처리
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Response: " + jsonResponse);

            // JSON 응답을 ArticlesData 객체로 역직렬화
            ArticlesData articlesData = JsonConvert.DeserializeObject<ArticlesData>(jsonResponse);

            // 각 Article의 데이터를 개별적으로 사용
            foreach (var article in articlesData.articles)
            {
                Debug.Log("Field: " + article.field);
                Debug.Log("Title: " + article.cleaned_body);
                Debug.Log("Body: " + article.cleaned_title);
                Debug.Log("Body: " + article.summary);
                Debug.Log("Body: " + article.image);

                // 필요 시 각각의 데이터를 사용 (예: UI에 표시 등)
                // 예: field, title, body 데이터를 UI나 다른 로직에 사용
                UseArticleData(article.field, article.cleaned_body, article.cleaned_body, article.summary, article.image);
            }
        }
        else
        {
            // 요청 실패 시 오류 메시지 출력
            Debug.LogError("Error: " + request.error);
        }
    }

    // article 데이터를 사용하는 예시 메서드
    void UseArticleData(string field, string cleaned_title, string cleaned_body, string summary, Image image)
    {
        // 여기서 각 데이터를 개별적으로 사용할 수 있습니다.
        // 예: UI 업데이트, 로직 처리 등
        Debug.Log($"Using Data - Field: {field}, cleande_Title: {cleaned_title}, Body: {cleaned_body}, Summary: {summary}, Image: {image}");
    }
}
