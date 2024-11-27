using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;
using TMPro;

public class AIReport : MonoBehaviour
{

    public TMP_Text playeridTMPText;
    public TMP_Text assetTotalTMPText;
    public TMP_Text avg_differenceTMPText;
    public TMP_Text improvementSuggestionsTMPText;
    public TMP_Text learningAnglyticsTMPText;
    public TMP_Text stock_avg_stocksTMPText;
    public TMP_Text analysisAssetStatusSummaryTMPText;
    public TMP_Text avg_savingTMPText;
    public TMP_Text AssetsSavingTMPText;
    public TMP_Text analysisAssetManagementStatusTMPText;
    public TMP_Text assetProduct_avg_productsTMPText;
    
    //원형 그래프 필요한 정보들
    //inves~ 주식 이름
    //Learning~ 퍼센트
    
    //꺽은선 그래프 필요한 정보들
    //assests.products 0(초기값같음)
    //cash 
    //assets.avg_assets
    //improvement~
    //avg_product 평균값 까만선
    
    private async void Start()
    {
        // GoogleSheetManager의 데이터가 로드될 때까지 기다립니다.
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);

        // AI 보고서를 가져옵니다.
        FetchAIReport();
    }

    private void FetchAIReport()
    {
        // GoogleSheetManager에서 "분석레포트"에 해당하는 URL 데이터를 가져옵니다.
        var urlData = GoogleSheetManager.Instance.UrldataGet("분석레포트");

        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("분석레포트 URL의 서버 주소가 비어있습니다.");
            return;
        }

        // 가져온 URL 데이터를 Post 요청에 사용하여 AI 보고서를 가져옵니다.
        PostAIReportFromAI(urlData.Server).Forget();
    }

    private async UniTask PostAIReportFromAI(string urlData)
    {
        // 서버로 보낼 데이터 생성 (예: id 값)
        var requestData = new { id = "1" }; // 보낼 데이터 설정
        string jsonString = JsonConvert.SerializeObject(requestData); 

        // UnityWebRequest 설정
        UnityWebRequest request = new UnityWebRequest(urlData, "POST"); 
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString); 
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer(); 
        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("요청을 보냅니다...");
        
        // 요청 보내기
        await request.SendWebRequest();  

        // 응답 처리
        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text; 
            Debug.Log("응답 받음: " + jsonResponse);

            try
            {
                // JSON 응답을 RootData 객체로 역직렬화
                RootData responseData = JsonConvert.DeserializeObject<RootData>(jsonResponse);

                if (responseData != null)
                {
                    ProcessResponse(responseData);
                    ReportGraph(responseData);

                }
                else
                {
                    Debug.LogError("응답 데이터를 처리하는 데 실패했습니다. JSON 구조를 확인하세요.");
                }
            }
            catch (JsonException ex)
            {
                Debug.LogError("JSON 파싱 중 오류 발생: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError($"요청 실패: {request.error}");
        }
    }

    // JSON 응답을 처리하는 메서드
    public void ProcessResponse(RootData responseData)
    {
        
        // 응답 데이터에서 player_id와 자산 정보 출력
        Debug.Log($"플레이어 ID: {responseData.raw_data.player_id}");

        var assets = responseData.raw_data.assets;

        Debug.Log($"총 자산: {assets.total}");
        Debug.Log($"현금: {assets.cash}");
        Debug.Log($"저축: {assets.savings}");

        var analysis = responseData.analysis;

        Debug.Log($"자산 현황 요약: {analysis.AssetStatusSummary}");
        Debug.Log($"자산 운용 현황: {analysis.AssetManagementStatus}");

        // 플레이어 이름
        playeridTMPText.text = responseData.raw_data.player_id;
        
        //내 소득은?
        assetTotalTMPText.text = responseData.raw_data.assets.total.ToString();
        avg_differenceTMPText.text = responseData.raw_data.assets.avg_difference.ToString();
        
        //소비
        improvementSuggestionsTMPText.text = responseData.analysis.ImprovementSuggestions;
        assetProduct_avg_productsTMPText.text = responseData.raw_data.assets.avg_products;
       
        //투자
        learningAnglyticsTMPText.text = responseData.analysis.LearningAnalytics;
        stock_avg_stocksTMPText.text = responseData.raw_data.assets.avg_stocks;
        
        //내 자산 분석
        analysisAssetStatusSummaryTMPText.text = responseData.analysis.AssetStatusSummary;
        
        //저축 금액
        avg_savingTMPText.text = responseData.raw_data.assets.avg_savings.ToString();
        AssetsSavingTMPText.text = responseData.raw_data.assets.savings.ToString();
        
        //종합평가
        analysisAssetManagementStatusTMPText.text = responseData.analysis.AssetManagementStatus;

    }

    public void ReportGraph(RootData responseData)
    {
        var g0 = responseData.raw_data.assets.products;
        var g1 = responseData.raw_data.assets.cash;
        var g2 = responseData.raw_data.assets.avg_assets;
        var g3 = responseData.analysis.ImprovementSuggestions;
        var g4 = responseData.raw_data.assets.avg_products; //평균값
        Debug.Log(g0);
        Debug.Log("그래프 레포트 테스트");

        var ra0 = responseData.raw_data.ratios.cash;
        var ra1 = responseData.raw_data.ratios.savings;
        var ra2 = responseData.raw_data.ratios.products;
        var ra3 = responseData.raw_data.ratios.stocks;
        Debug.Log(ra3);
        Debug.Log("라티오 레포트 테스트");

        var one0 = responseData.analysis.InvestmentPropensityAnalysis;
        var one1 = responseData.analysis.LearningAnalytics;
        Debug.Log(one1);
        Debug.Log("원형그래프");
    }
}
