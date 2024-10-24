using UnityEngine;

public class GovernmentManager : MonoBehaviour
{
    // 날짜 정보 받아서 월급, 물가, 뭐시기 등등 변환해주기

    //private int dayOffset = 0;
    private int weekOffset = 1996;
    
    private void Awake() // 나중에 디스폰 될 때 이벤트 삭제해주기
    {
        //Debug.Log("정부 시스템 돌아가용");
        //RoundSystem.Instance.onDayChanged += OnDayChanged;
        RoundSystem.Instance.onWeekChanged += OnWeekChanged; // 이벤트를 추가한 것일 뿐 델리게이트를 어디선가 불러줘야함.
    }

    private void OnDayChanged(int Day)
    {
        Debug.Log("정부에서 매일 바꾸는 거");
        // 주식이 바뀐당.  
        GoogleSheetManager.Instance.YearlyDataGet(Day); //이거 아님, 주식 데이터로 변환 필요 
    }

    private void OnWeekChanged(int Week)
    {
        Debug.Log($"주: {Week}");
        //Debug.Log("정부에서 주마다 바뀌는거");
        int year = weekOffset + Week;
        var newData = GoogleSheetManager.Instance.YearlyDataGet(year);
        
        Debug.Log($"월급: {newData.Salary}");
    }
}
