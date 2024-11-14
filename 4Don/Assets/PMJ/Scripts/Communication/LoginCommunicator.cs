using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json; 
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class LoginCommunicator : MonoBehaviour
{
    public LoginManager loginManager;
    public AudioSource audioSource;

    public static string Value;
    private async void StartLoginProcess() 
    {
        await (LoginStart());
    }
    
    private async UniTask LoginStart()
    {
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);

        var urlData = GoogleSheetManager.Instance.UrldataGet("로그인");
        
        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("로그인 URL의 서버 주소가 비어있습니다.");
            return;
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
            string rep = request.downloadHandler.text;
            Debug.Log(rep);
            SceneManager.LoadScene("SsamMade");
            DontDestroyOnLoad(audioSource);
            
            Value = rep.ToString();

        }
        else
        {
            Debug.LogError("Error sending data: " + request.error);
        }
    }
    
    public void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    
    
}