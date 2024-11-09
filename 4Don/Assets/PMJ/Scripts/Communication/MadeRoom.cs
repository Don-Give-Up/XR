using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MadeRoom : MonoBehaviour
{
    public MadeRoomData madeRoomData;

  
    public void RoomMade()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        var urlData = GoogleSheetManager.Instance.UrldataGet("방만들기");
        
        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("방만들기 URL의 서버 주소가 비어있습니다.");
            yield break;
        }
        else
        {
            Debug.Log("방만들기 url 받아짐");
        }
        
        Debug.Log(urlData.Name);
        Debug.Log(urlData.Server);
        // MadeRoomData에서 입력된 데이터를 가져와 PostMadeRoomData 객체로 변환
        PostMadeRoomData requestData = madeRoomData.GetRoomMade();
        Debug.Log(requestData.roomName);
        Debug.Log(requestData.roomPassword);
        StartCoroutine(RoomMadePost(urlData.Server, requestData));
    }

    // 데이터를 POST로 보내고 응답을 받아오는 코루틴
    IEnumerator RoomMadePost(string url, PostMadeRoomData requestData)
    {
        

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
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Response: " + jsonResponse);
            
            // 서버의 응답을 PostMadeRoomData 객체로 역직렬화 (필요 시)
            //PostMadeRoomData postMadeRoomData = JsonConvert.DeserializeObject<PostMadeRoomData>(jsonResponse);
            // 서버 응답 데이터를 사용하여 필요한 작업 수행
            //역직렬화할 필요 없을거 같음. 
        }
        else
        {
            // 요청 실패 시 오류 메시지 출력
            Debug.LogError("Error: " + request.error);
        }
    }
}
