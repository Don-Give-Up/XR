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
    //public TMP_Text field;
    public TMP_Text[] title;
    public TMP_Text[] summary;
    //public TMP_Text body;
    // public RawImage AIImage;
    
    private List<string> fieldList = new List<string>();
    private List<string> titleList = new List<string>();
    private List<Texture2D> imageList = new List<Texture2D>();

    public string AritcleDay;
    public void Start()
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
        var requestData = new Dictionary<string, string>
        {
            { "quarter", "19964" }
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
    Texture2D ConvertBase64ToTexture(string base64Image)
    {
        // Base64 문자열을 byte 배열로 변환
        byte[] imageBytes = System.Convert.FromBase64String(base64Image);

        // Texture2D 생성 및 이미지 데이터 로드
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        return texture;
    }

    private void UseArticlesData(ArticlesData articlesData)
    {
        // 각 Article의 데이터를 개별적으로 사용
        for (int i = 0; i < articlesData.articles.Count; ++i)
        {
            var article = articlesData.articles[i];
            
            /*Debug.Log("Field: " + article.field);
            Debug.Log("Title: " + article.cleaned_title);
            Debug.Log("Body: " + article.cleaned_body);
            Debug.Log("Summary: " + article.summary_2_lines);
            Debug.Log("Summary_50: " + article.summary_50);*/
            // Base64 이미지 데이터를 Texture2D로 변환
            if (!string.IsNullOrEmpty(article.image))
            {
                article.texture = ConvertBase64ToTexture(article.image);
                Debug.Log("Image successfully converted.");
            }
            else
            {
                Debug.Log("No image found.");
            }
            
            //값 저장
            //fieldList.Add(article.field);
            titleList.Add(article.cleaned_title);
            imageList.Add(article.texture);
            
            //field.text = article.field;
            title[i].text = article.cleaned_title;
            summary[i].text = article.summary_50;

            // 필요 시 데이터를 UI에 표시하거나 다른 로직에 사용
            UseArticleData(i, article);
        }
    }
    
    
    void UseArticleImage(Texture2D image)
    {
        // 예를 들어, UI에 표시하기 위해 RawImage 컴포넌트를 사용하는 경우
        //RawImage imageComponent = GetComponent<RawImage>();
        //AIImage.texture = image;    
    }


    // article 데이터를 사용하는 예시 메서드
    private void UseArticleData(int index, Article article)
    {
        UseArticleImage(article.texture);
        
        // 여기서 각 데이터를 개별적으로 사용할 수 있습니다.
        // 예: UI 업데이트, 로직 처리 등
        Debug.Log($"Using Data - Field: {article.field}, cleande_Title: {article.cleaned_title}, Body: {article.cleaned_body}, Summary: {article.summary_50}");
        
    }
    
    
    
}
