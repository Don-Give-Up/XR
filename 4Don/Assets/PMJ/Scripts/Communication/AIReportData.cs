using Newtonsoft.Json;

[System.Serializable]
public class ResponseData
{
   
    public RawData raw_Data;
    public string analysis;
}

[System.Serializable]
public class RawData
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
}

[System.Serializable]
public class Ratios
{
    public float cash;
    public float savings;
    public float products;
    public float stocks;
}

[System.Serializable]
public class WrongAnswer
{
    public string quiz;
    public string answer;
    public string desc;
}