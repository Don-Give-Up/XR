using Newtonsoft.Json;

[System.Serializable]
public class ResponseData
{
    public RawData raw_data; // JSON의 "raw_data" 키와 매핑
    public Analysis analysis; // JSON의 "analysis" 키와 매핑
}

[System.Serializable]
public class RawData
{
    public int player_id; // 플레이어 ID
    public Assets assets; // 자산 정보
    public Ratios ratios; // 비율 정보
}

[System.Serializable]
public class Assets
{
    public float total; // 총 자산
    public float cash; // 현금
    public float savings; // 저축
    public float products; // 상품
    public float stocks; // 주식
    public float avg_difference; // 평균 대비 금액
}

[System.Serializable]
public class Ratios
{
    public float cash; // 현금 비율
    public float savings; // 저축 비율
    public float products; // 상품 비율
    public float stocks; // 주식 비율
}

[System.Serializable]
public class Analysis
{
    
    public string AssetStatusSummary; // 자산 현황 요약
    public string AssetManagementStatus; // 자산 운용 현황
    public string InvestmentPropensityAnalysis; // 투자 성향 분석
    public string LearningAnalytics; // 금융 이해도 진단
    public string ImprovementSuggestions; // 개선 제안
}