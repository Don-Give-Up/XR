using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PersonalFinancialManager : MonoBehaviour
{
    // 입금 . 출금 기능 
    
    // 입금 상황 = 월급, 창작물, 등 등 등  + -> 주단위 변화와 동일함 but 이걸 돈이 변경되는 이벤트와 합쳐도 됨
    // 출금 상황 = 소비, 투자, 등 등 등  - -> 이벤트로 만들어도 됨
    // 합치자!

    //public Action<double> onMoneyChanged; 

    public double currentMoney = 0;

    public TMP_Text currentMoneyText;

    private double displayMoney = 0;

    private float duration = 1.5f; 
    
    public static PersonalFinancialManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        currentMoney = 0;
        
        MoneyText(currentMoney).Forget();
        //MoneyText(currentMoney);
        //onMoneyChanged += OutputMoney;
        
    }
    
    public void InputMoney(double inMoney)
    {
        currentMoney += inMoney;
        Debug.Log($"보유 현금 : {currentMoney}"); 
        MoneyText(currentMoney).Forget();
        //MoneyText(currentMoney);
    }

    public void OutputMoney(double outMoney)
    {
        currentMoney -= outMoney;
        Debug.Log($"보유 잔액 : {currentMoney}"); 
        MoneyText(currentMoney).Forget();
        /*MoneyText(currentMoney);*/
    }
    
    private async UniTaskVoid MoneyText(double targetMoney)
    {
        float timeElapsed = 0F;
    
        SoundManager sound = gameObject.GetComponentInChildren<SoundManager>();
        sound.SoundPlay();
        
        // 현재 값 displayMoney로 시작하여 targetMoney로 변경
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
        
            // Lerp 함수에 float으로 변환해서 전달
            double currentValue = LerpDouble(displayMoney, targetMoney, t); 

            // 현재 값(변화된 displayMoney)을 텍스트로 업데이트
            currentMoneyText.text = $"{currentValue:N0}원"; // 천 단위 구분을 위해 N0 포맷 사용

            timeElapsed += Time.deltaTime;


            // UniTask.Delay로 지연을 주며 애니메이션 처리
            await UniTask.Delay(1); // 1프레임 지연
        }
    
        // 마지막 값으로 확실히 설정
        currentMoneyText.text = $"{targetMoney:N0}원";
    
        // 최종 값인 targetMoney로 displayMoney를 업데이트
        displayMoney = targetMoney;
    }

    // double 타입의 Lerp 함수 (선택 사항)
    private double LerpDouble(double startValue, double endValue, float t)
    {
        return startValue + (endValue - startValue) * t;
    }

    /*private void MoneyText(double currentMoney)
    {
        currentMoneyText.text = $"{currentMoney:N0}원";
    }*/
    // 돈에 관련된 거 text랑 연결

}
