using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// 년+날짜값 가지고 오기 + 1 >> 보낼 데이터
/// 
/// </summary>
public class AIAritclePost : MonoBehaviour
{

    public News1[] news1;
    

    //public News1[] news1;
    //public TMP_Text field;
    public TMP_Text[] title;
    public TMP_Text[] summary;

    public TMP_Text[] cleaned_body;

    //public TMP_Text[] field;
    public RawImage[] images;

    //public TMP_Text body;
    // public RawImage AIImage;

    private List<string> fieldList = new List<string>();
    private List<string> titleList = new List<string>();
    private List<Texture2D> imageList = new List<Texture2D>();

    private int articleYear; //  몫
    private int articleDay; //나누기
    private int offset = 1996;

    public string aritcleDayText;

    /*private void Awake()
    {
        RoundSystem.Instance.onDayChanged += AiPostDataDay;
    }

    private void AiPostDataDay(int days) // 바뀌는 날마다 데이터 받아올거임
    {
        Debug.Log("날실행!!");
        articleYear = (days / 5) + offset;
        articleDay = (days % 5) + 1;

        aritcleDayText = articleYear.ToString() + articleDay.ToString();

        Debug.Log($"{aritcleDayText}");
        StartCoroutine(Test());
    }
    */

    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(3f);
        var urlData = GoogleSheetManager.Instance.UrldataGet("뉴스데이터");


        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("뉴스데이터 URL의 서버 주소가 비어있습니다.");
            yield break;
        }
        else
        {
            Debug.Log("뉴스데이터 url 받아짐");
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
            { "year", 2020 }
        };

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
            UseArticlesData(articlesData);
        }
        else
        {
            // 요청 실패 시 오류 메시지 출력
            Debug.LogError("Error: " + request.error);
        }
    }


    // 각 Article의 데이터를 개별적으로 사용
    private void UseArticlesData(ArticlesData articlesData)
    {
        // 개별 Article 데이터를 사용
        news1[0].UseData(articlesData.economy);
        news1[1].UseData(articlesData.finance);
    }


}

