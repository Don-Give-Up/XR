using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json; 

public class BEPost : MonoBehaviour
{
    // 서버의 URL을 정의
    string postUrl = "http://125.132.216.190:5678/api/members"; // Post 요청을 보낼 엔드포인트

    // 유니티에서 처음 실행될 때 호출
    void Start()
    {
        StartCoroutine(PostMemberData()); // Coroutine 실행
    }

    // 멤버 데이터를 서버로 POST 방식으로 보내는 코루틴
    IEnumerator PostMemberData()
    {
        // Postman에서 설정한 JSON 형식의 데이터를 객체로 생성
        var memberData = new
        {
            memberEmail = "kch4731@naver.com",
            memberSchool = "태봉초등학교",
            memberName = "김채호",
            memberGrade = 6,
            memberClass = 1,
            memberNumber = 8
        };

        // 데이터를 JSON 문자열로 변환
        string jsonData = JsonConvert.SerializeObject(memberData);

        // POST 요청 생성 (URL, 메서드 지정)
        UnityWebRequest request = new UnityWebRequest(postUrl, "POST");

        // JSON 데이터를 바이트로 변환하여 업로드 핸들러에 설정
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Content-Type 헤더 설정 (JSON 데이터를 전송하므로)
        request.SetRequestHeader("Content-Type", "application/json");

        // 서버에 요청을 보내고 응답을 기다림
        yield return request.SendWebRequest();

        // 요청이 성공했는지 확인
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 성공 시 응답 데이터 출력
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            // 실패 시 오류 메시지 출력
            Debug.LogError("Error: " + request.error);
        }
    }
}
