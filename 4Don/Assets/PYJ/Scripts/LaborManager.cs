using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class LaborManager : MonoBehaviour
{
    private bool laborChecked = false;
    private bool[] laborDay = new bool[5];

    private int gameDay = 0;
    private int laborTime = 0;
    private int year = 1996;
    
    private double salary = 0;
    
    //하루 끝이 아니라 노동이 발생하는 타이밍을 감지할 수 있나? 
    private void Awake()
    {
        StartCoroutine(Initialization());
    }

    private IEnumerator Initialization()
    {
        yield return new WaitUntil(() => RoundSystem.Instance.isLoaded);
        //laborDay = new bool[RoundSystem.Instance.oneWeekTime]; // 5개 만들어짐 
        RoundSystem.Instance.onDayChanged += OnLaborChecked;
        RoundSystem.Instance.onWeekChanged += OnSalaryChanged; 
    }

    private void OnLaborChecked(int day)
    {
        // 날이 지날 때 노동을 했는지 안 했는지를 확인한다.(노동은 날짜를 바탕으로 해야할 것 같음)
        // 1. 민주 쪽 노동 확인 실행 
        // 2. 저장하고 초기화 해줌 
        // 3. 정보 저장해서 넘겨줌
        Debug.Log("일 했니 안 했니");
        laborChecked = true;//Quiz.Instance.OnLaborCheak(); // 당일에 해당하는 정보

        gameDay = day % RoundSystem.Instance.oneWeekTime; // 몇 번째 요일?에 해당하는지 

        laborDay[gameDay] = laborChecked;

        Quiz.Instance.onlaborCheak = false; // 값 저장한 다음 초기화 
    }

    private void OnSalaryChanged(int week)
    {
        year += week;
        
        Debug.Log("월급 줄게용");

        //일한 날짜 카운트
        LaborTimeCount(); 
        
        // 월급 얼마 줘야하는지 카운트 
        SalaryCount(year); 
        
        // 초기화 
        ResetSalary(); 
        
    }

    private void LaborTimeCount()
    {
        for (int i = 0; i < laborDay.Length; i++)
        {
            if (laborDay[i] == true)
            {
                Debug.Log("일 했구낭");
                
                laborTime++;
            }
        }
    }

    private void SalaryCount(int year)
    {
        double yearlySalary = GoogleSheetManager.Instance.YearlyDataGet(year).Salary;
        salary = (yearlySalary / RoundSystem.Instance.oneWeekTime) * laborTime;
        
        Debug.Log($"월급 입금 : {salary}");
    }

    private void ResetSalary()
    {
        Debug.Log("초기화 할라고");

        year = 1996;
        laborTime = 0;
        salary = 0;
        Array.Clear(laborDay, 0, laborDay.Length);
        
        Debug.Log(string.Join(",",laborDay));
    }
}
