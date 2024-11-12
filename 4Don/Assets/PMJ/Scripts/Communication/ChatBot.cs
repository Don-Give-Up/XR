using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChatBot : MonoBehaviour
{
    public TMP_InputField question;
    public GameObject mePrefab;
    public GameObject youPrefab;
    public GameObject parentPosition;
    

    private string _url;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        var urlData = GoogleSheetManager.Instance.UrldataGet("챗봇");
        
        
        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("챗봇 URL의 서버 주소가 비어있습니다.");
            yield break;
        }
        else
        {
            Debug.Log("챗봇 url 받아짐");
        }
        Debug.Log(urlData.Name);
        Debug.Log(urlData.Server);
        _url = urlData.Server;

    }

    public void SendMessage()
    {
        StartCoroutine(PostChatBotQuestion(_url));
        
    }
    // 데이터를 POST로 보내고 응답을 받아오는 코루틴
    private IEnumerator PostChatBotQuestion(string url)
    {
        string quest = question.text;
        Debug.Log(quest);
        // 서버에 보낼 데이터
        var requestData = new Dictionary<string, string>
        {
            { "query", quest }
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
        
        Me(quest);
        question.text = "";
        // 요청이 성공했는지 확인
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 성공 시 서버로부터 받은 응답 처리
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Response: " + jsonResponse);

            // JSON 응답을 ChatBotData 객체로 역직렬화
            ChatBotData chatBotData = JsonConvert.DeserializeObject<ChatBotData>(jsonResponse);
            You(chatBotData.Result);
            Debug.Log(chatBotData.Result);
        }
        else
        {
            // 요청 실패 시 오류 메시지 출력
            Debug.LogError("Error: " + request.error);
        }
    }

  
    private void Me(string queryText)
    {
        //여기서 내 말 생성해주기   
        var youText = Instantiate(mePrefab, parentPosition.transform);
        var text = youText.GetComponentInChildren<TMP_Text>();
        text.text = queryText;
    }

    private void You(string resultText)
    {
        //여기서 니 말 생성 해주기
        var youText = Instantiate(youPrefab, parentPosition.transform);
        var text = youText.GetComponentInChildren<TMP_Text>();
        text.text = resultText;
    }
    //읭? 지금 텍스트 받아온건 어디로 주지?
}
