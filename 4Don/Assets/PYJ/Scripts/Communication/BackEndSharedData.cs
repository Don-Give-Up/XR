using System.Collections.Generic;
using UnityEngine;

public class BackEnd
{
    // 아마 다 배열 정보들로 바뀔 것 같음 
    public int[,] RoundInfo;
    public string UserInfo;
    public double SalaryInfo;
    public double ConsumptionInfo;
    public double InvestmentInfo;

    public BackEnd(int[,] roundInfo, string userInfo, double salaryInfo, double consumptionInfo,
        double investmentInfo)
    {
        RoundInfo = roundInfo;
        UserInfo = userInfo;
        SalaryInfo = salaryInfo;
        ConsumptionInfo = consumptionInfo;
        InvestmentInfo = investmentInfo;
    }
}

public class BackEndSharedData : MonoBehaviour
{
    // 나중에 숨기기
    public static Dictionary<int[,], BackEnd> backEnds = new Dictionary<int[,], BackEnd>();

    public static BackEndSharedData Instance;
    
    //날짜와 요소들을 정해서 백앤드에게 정해주면 됨
    //만약 이벤트 발생 순간에 통신할 거면 구조체 분리
    //이 방식은 날짜가 변경되는 시점을 기준으로 작성하였음



}
