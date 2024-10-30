using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PersonalFinancialManager : MonoBehaviour
{
    // 입금 . 출금 기능 
    
    // 입금 상황 = 월급, 창작물, 등 등 등  + -> 주단위 변화와 동일함 but 이걸 돈이 변경되는 이벤트와 합쳐도 됨
    // 출금 상황 = 소비, 투자, 등 등 등  - -> 이벤트로 만들어도 됨
    // 합치자!

    //public Action<double> onMoneyChanged; 

    public double currentMoney = 5000;

    public TMP_Text currentMoneyText;
    
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
        
        currentMoney = 5000;
        MoneyText(currentMoney);
        //onMoneyChanged += OutputMoney;
        
    }
    
    public void InputMoney(double inMoney)
    {
        currentMoney += inMoney;
        Debug.Log($"보유 현금 : {currentMoney}"); 
        MoneyText(currentMoney);
    }

    public void OutputMoney(double outMoney)
    {
        currentMoney -= outMoney;
        Debug.Log($"보유 잔액 : {currentMoney}"); 
        MoneyText(currentMoney);
    }

    private void MoneyText(double currentMoney)
    {
        currentMoneyText.text = $"보유현금 {currentMoney:N0}원";
    }
    // 돈에 관련된 거 text랑 연결

}
