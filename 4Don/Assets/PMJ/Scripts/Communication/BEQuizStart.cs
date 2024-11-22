using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class BEQuizStart : MonoBehaviour
{
    public BEQuiz quiz;
    public static QuizData[] BEQuizdata;
    public async void Start() // 퀴즈 먼저 읽어오기
    {
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);
        
        var urlData = GoogleSheetManager.Instance.UrldataGet("퀴즈데이터");
        
        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("퀴즈데이터 URL의 서버 주소가 비어있습니다.");
            return;
        }

        
        GetQuizDataFromUrl(urlData.Server).Forget();

    }
    
    private async UniTask GetQuizDataFromUrl(string url)
    {
      
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization",LoginCommunicator.Value);
            //request.SetRequestHeader("Authorization", "Bearer eyJkYXRlIjoxNzMwNzEyNjA4NTY4LCJ0eXBlIjoiand0IiwiYWxnIjoiSFMyNTYifQ.eyJzdWIiOiJ0b2tlbiA6IDgiLCJtZW1iZXJTY2hvb2wiOiJzY2hvb2wiLCJtZW1iZXJHcmFkZSI6MywibWVtYmVyTmFtZSI6Im5hbWUiLCJtZW1iZXJOaWNrbmFtZSI6Im5pY2tuYW1lIiwiZXhwIjoxNzYyMjQ4NjA4LCJtZW1iZXJSb2xlIjoiU1RVREVOVCIsIm1lbWJlckNsYXNzIjozLCJtZW1iZXJJZCI6OCwibWVtYmVyRW1haWwiOiJlbWFpbCJ9.dxvJMBF88sWHvsPLosKjD4jbgzDPh_-ROUZ7U8vpMW4"); // test
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("퀴즈 데이터를 가져오는 중 오류 발생: " + request.error);
            }
            else
            {
                // JSON 데이터를 받아옴
                string jsonQuizData = request.downloadHandler.text;

                try
                {
                    // JSON 데이터를 QuizData 객체 배열로 변환
                    BEQuizdata = JsonConvert.DeserializeObject<QuizData[]>(jsonQuizData);

                    if (BEQuizdata != null && BEQuizdata.Length > 0)
                    {
                       
                        Debug.Log($"총 {BEQuizdata.Length}개의 퀴즈 데이터를 불러왔습니다.");
                    }
                    else
                    {
                        Debug.LogError("퀴즈 데이터를 불러오는 데 실패했습니다.");
                    }
                }
                catch (JsonReaderException ex)
                {
                    Debug.LogError("JSON 파싱 중 오류 발생: " + ex.Message);
                }
            }
        }
    }

}
