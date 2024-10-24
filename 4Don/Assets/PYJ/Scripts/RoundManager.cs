using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    // 매일 매일을 체크하고 5일 째 되는날마다 월급 지급하게 하고 
    // 그 정보를 바탕으로 정부 시스템 작동 
    // 1. 게임 진행 하루 = 현실 시간 5분 
    // 2. 라운드 =  하루 * 5번 
    // 3. 주식은 하루마다 갱신 
    // 4. 나머지는 라운드마다 갱신 
    
    // 선생님한테 종속되게 하면될 듯?
    // 나중에 네트워크 공유 데이터에 전달할 것 
    
    private int oneWeekTime = 5; //5회
    private int oneDay = 5 * 60; //5분 
    
    private int maxWeek = 28;
    private int maxDay = 5 * 28; // oneWeekTime * maxWeek //이거 수정해야 함~

    private int currentDay = 0;
    private int currentWeek = 0;

    public Action<int> onDayChanged;
    public Action<int> onWeekChanged;

    public static RoundSystem Instance;

    private void Awake()
    {
        //RoundSystem.Instance 가 null 인 걸 대비
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() //선생님이 들어와서 아이들도 참가하고 진행버튼 누르면 실시되게 하기
    { 
        Process();
    }

    private void Process()
    {
        //Debug.Log(" 진행시켜");
        
        StartCoroutine(Day());
        
        if (currentDay == 0 || currentDay % 5 == 0)
        {
            Week();
        }
    }

    private IEnumerator Day()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("선생님이 동작함");
            Debug.Log($"Day: {currentDay}");
            currentDay++;
            Process();
        }

        //Debug.Log(" 하루 계산기 돌아가용");
        onDayChanged?.Invoke(currentDay);
        //yield return new WaitForSecondsRealtime(oneDay); // 실제론 5분
        yield return new WaitForSecondsRealtime(0.1f);
        Debug.Log($"Day: {currentDay}");
        currentDay++;

        if (currentDay == maxDay)
        {
            End();
        }
        
        Process();

    }

    private void Week()
    { 
        //Debug.Log("주 계산기 돌아가용");
        onWeekChanged?.Invoke(currentWeek);
        Debug.Log($"Week: {currentWeek}"); // 왜 currentWeek == 0 일떄 실행이 안 돼지?
        currentWeek++;
        
        // 분석 리포트도 제공해야 함 그게 끝날 때까지 잡고 있어야 할 듯 한디
    }

    private void End()
    {
        Debug.Log("게임 끝남");
        // 게임 끝까지 갔을 때 할 거 
    }
}
