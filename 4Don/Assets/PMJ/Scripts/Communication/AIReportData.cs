[System.Serializable]
public class AIReportResponseData
{
    public AIReportRawData aiReportRawData;
    public string analysis;
}

[System.Serializable]
public class AIReportRawData
{
    public int player_id;
    public Assets assets;
    public Ratios ratios;
    public WrongAnswer[] wrong_answers;
}

[System.Serializable]
public class Assets
{
    public float total;
    public float cash;
    public float savings;
    public float products;
    public float stocks;
    public float avg_difference;
}

[System.Serializable]
public class Ratios
{
    public float cash;       // 현금
    public float savings;    // 저축
    public float products;   // 상품
    public float stocks;     // 주식
}


[System.Serializable]
public class WrongAnswer
{
    public string quiz;
    public string answer;
    public string desc;
    public string category;
}