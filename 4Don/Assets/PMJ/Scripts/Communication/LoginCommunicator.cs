using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json; 
using System.Text;

public class LoginCommunicator : MonoBehaviour
{
    public LoginManager loginManager;

    public static string Value;
    public void StartLoginProcess() 
    {
        StartCoroutine(LoginStart());
    }
    
    private IEnumerator LoginStart()
    {
        var urlData = GoogleSheetManager.Instance.UrldataGet("로그인");
        
        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("로그인 URL의 서버 주소가 비어있습니다.");
            yield break;
        }
        else
        {
            Debug.Log("로그인 URL 받아짐");
        }

        LoginData reqInfo = loginManager.GetLogin();  // 로그인 데이터를 수집한 후
        StartCoroutine(PostLoginRequest(urlData.Server, reqInfo));
    }

    public IEnumerator PostLoginRequest(string uri, LoginData reqInfo)
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
            LoginData rep = JsonConvert.DeserializeObject<LoginData>(jsonResponse);
            Debug.Log(rep);

            Value = rep.ToString();

        }
        else
        {
            Debug.LogError("Error sending data: " + request.error);
        }
    }
}