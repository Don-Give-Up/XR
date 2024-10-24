using System;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class LaborManager : MonoBehaviour
{
// 민주스크립트.인스턴스 

    private bool laborChecked = false;
    private void Start()
    { 
        RoundSystem.Instance.onDayChanged += OnLaborChecked;
        RoundSystem.Instance.onWeekChanged += OnSalaryChanged; // 일자별로 매일매일 체크하거나
    }

    private void OnLaborChecked(int day)
    {
        // 일단 노동을 끝내면 노동 상황이 변해음을 전달한다
        // 날이 지날 떼 노동을 했는지 안 했는지를 확인한다. -> 이제 더 나을 듯?
        // 1. 민주 쪽 노동 확인 실행 
        // 2. 저장하고 초기화 해줌 
        // 3. 정보 저장해서 넘겨줌
    }

    private void OnSalaryChanged(int week)
    {
        // 일한 일자를 바탕으로 월급 지급하기   
    }

}
