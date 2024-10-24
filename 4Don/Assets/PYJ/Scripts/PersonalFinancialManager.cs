using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PersonalFinancialManager : MonoBehaviour
{
    // 입금 . 출금 기능 
    
    // 입금 상황 = 월급, 창작물, 등 등 등  + -> 주단위 변화와 동일함 but 이걸 돈이 변경되는 이벤트와 합쳐도 됨
    // 출금 상황 = 소비, 투자, 등 등 등  - -> 이벤트로 만들어도 됨
    // 합치자!

    public Action<double> onMoneyChanged; 

    private double currentMoney = 0;
    
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

        onMoneyChanged += OutputMoney;
    }
    
    public void InputMoney(double inMoney)
    {
        currentMoney += inMoney;
        Debug.Log($"보유 현금 : {currentMoney}"); 
    }

    public void OutputMoney(double outMoney)
    {
        if (currentMoney < outMoney) // 자산 보유 마이너스 
        {
            // 이 떄 물건 안 사지도록
            // 이 떄 물건 안 사지도록
        }
        
        // 이 때 물건 사지도록
        currentMoney -= outMoney;
    }

    // 돈에 관련된 거 text랑 연결

}
