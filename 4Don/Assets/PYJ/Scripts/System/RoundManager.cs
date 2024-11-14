using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
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
    
    //d이게 없어지고 
    //년도로만 진행
    ////시연은 5분이므로 년도도 5분진행
    // 전부 하루에 대한건데 week가 년도 데이터를 기반으로 함. offset쪽을 2020년으로 바꾸면 될듯.
    // 시연용으로 따로 스크립트 짜야할 수 도 있음. 이벤트 연결해놓으면. 
    
    public int oneWeekTime = 5; //5회
    private int oneDay = 5 * 60; //5분 
    
    private int maxWeek = 28;
    private int maxDay = 5 * 28; // oneWeekTime * maxWeek //이거 수정해야 함~

    private int currentDay = 4;
    private int currentWeek = 0;

    private int yearOffset = 1996;

    //public int[,] round;

    //public Action<int> onDayChanged; 
    //public Action<int> onWeekChanged;

    private int currentRound = 0;
    private int currentRoundOffset = 2020; 
    private bool nextRound; 
    public Action<int> onRoundChange; 

    public bool isLoaded = false;

    /*
    public GameObject dayObject; 
    public TMP_Text dayText;
    public GameObject weekObject;
    public TMP_Text weekText;
    */

    private bool nextDay;
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
            Destroy(this);
        }
    }

    private void Start() //선생님이 들어와서 아이들도 참가하고 진행버튼 누르면 실시되게 하기
    {
        //round = new int[maxWeek, oneWeekTime]; // 주를 행으로 하루를 열로 하는 2차원 배열 생성
        RoundProcess();
        //StartCoroutine(Process());
    }

    private async UniTask RoundProcess()
    {
        isLoaded = true;
        
        await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);
        
        //일단 누르면 라운드 시스템 발동하도록
        Debug.Log($"현재 라운드: {currentRound + currentRoundOffset}");
        onRoundChange?.Invoke(currentRound + currentRoundOffset); // 현재 라운드 정보 제공

        currentRound++; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            nextRound = true;
        }
    }

    private IEnumerator Process()
    {
        isLoaded = true;
        Debug.Log("진행시켜");


        yield return new WaitUntil(()=>GoogleSheetManager.Instance.IsLoaded);

        // 줘야하는 값의 형태 구조체 만들고 키 값으로 사용하는 딕셔너리 만들면 될 듯
        //round[currentWeek, currentDay] = 10;
        
        //onDayChanged?.Invoke(currentDay);
        
        StartCoroutine(DayText(currentDay)); 
        
        StartCoroutine(Day());
        // 처음 시작할 떄 
        if (currentDay % 5 == 0)
        {
            //onWeekChanged?.Invoke(currentWeek + yearOffset);
            Week();
        } 
        StartCoroutine(WeekText(currentWeek));
        
    }

    private IEnumerator Day()
    {
        Debug.Log(" 하루 계산기 돌아가용");
        
        //yield return new WaitForSecondsRealtime(oneDay); // 실제론 5분 
        //yield return new WaitForSecondsRealtime(120f); // ㅌㅔ스트 코드
        yield return new WaitUntil(() => nextDay);
        nextDay = false;
        currentDay++;
        
        Debug.Log($"Day: {currentDay}");

        if (currentDay == maxDay)
        {
            End();
        }
        
        StartCoroutine(Process());
    }

    private IEnumerator DayText(int currentDay)
    {
        int day = this.currentDay % 5;
        string dayText = "";
        
        switch (day)
        {
            case 0:
                dayText = "월요일";
                break;
            case 1:
                dayText = "화요일";
                break;
            case 2:
                dayText = "수요일";
                break;
            case 3 :
                dayText = "목요일";
                break;
            case 4 :
                dayText = "금요일";
                break;
        }

        //dayObject.SetActive(true);
        //this.dayText.text = dayText;
        
        yield return new WaitForSecondsRealtime(4f);

        //dayObject.SetActive(false);
        
    }

    private void Week()
    { 
        Debug.Log("주 계산기 돌아가용");
        currentWeek++;
        Debug.Log($"Week: {currentWeek}"); // 왜 currentWeek == 0 일떄 실행이 안 돼지?
        // 분석 리포트도 제공해야 함 그게 끝날 때까지 잡고 있어야 할 듯 한디
    }
    
    private IEnumerator WeekText(int currenWeek)
    {
        string weekText = $"{currentWeek+1}주차";
        
        //weekObject.SetActive(true);
        //this.weekText.text = weekText;
        
        yield return new WaitForSecondsRealtime(4f);

        //weekObject.SetActive(false);
    }
    
    private void End()
    {
        Debug.Log("게임 끝남");
        // 게임 끝까지 갔을 때 할 거 
    }
}
