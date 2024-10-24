using System;
using UnityEngine;

public class SalaryManager : MonoBehaviour
{
   // 아이들이 일한 횟수에 따라 월급 지급 
   // 1. 아이들이 일한 횟수를 카운팅한다. 
   // 2. 일한 일수는 저장이 되어야 한다.
   // 3. 주가 바뀌면 주급 지급 

   private void Start()
   { 
       RoundSystem.Instance.onDayChanged += OnLaborChecked;
       RoundSystem.Instance.onWeekChanged += OnSalaryChanged; // 일자별로 매일매일 체크하거나
   }

   private void OnLaborChecked(int day)
   {
       // 일단 노동을 끝내면 노동 상황이 변해음을 전달한다
       // 날이 지날 떼 노동을 했는지 안 했는지를 확인한다. -> 이제 더 나을 듯? 
       
   }

   private void OnSalaryChanged(int week)
   {
     // 일한 일자를 바탕으로 월급 지급하기   
   }
}
