using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json; 
using System.Text;

public class SignUpCommunicator : MonoBehaviour
{
    public LoginManager loginManager;

    public void StartSignUpProcess() 
    {
        StartCoroutine(SignUpStart());
    }
    
    private IEnumerator SignUpStart()
    {
        var urlData = GoogleSheetManager.Instance.UrldataGet("회원가입");
        
        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("회원가입 URL의 서버 주소가 비어있습니다.");
            yield break;
        }
        else
        {
            Debug.Log("회원가입 URL 받아짐");
        }

        LoginRequest reqInfo = loginManager.GetSignUp();  // 로그인 데이터를 수집한 후
        StartCoroutine(PostSignUpRequest(urlData.Server, reqInfo));
    }

    private IEnumerator PostSignUpRequest(string uri, LoginRequest reqInfo)
    {
        // LoginManager를 JsonConvert를 사용해 JSON으로 변환
        var jsonData = JsonConvert.SerializeObject(reqInfo);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        UnityWebRequest request = new UnityWebRequest(uri, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data sent successfully: " + request.downloadHandler.text);
            
            // request.downloadHandler.text -> (Deserialize) -> LoginResponse
            string jsonResponse = request.downloadHandler.text;
            LoginResponse rep = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
            Debug.Log(rep.memberId);
        }
        else
        {
            Debug.LogError("Error sending data: " + request.error);
        }
    }
}