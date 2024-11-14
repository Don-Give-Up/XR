using System;
using UnityEngine;

public class MoneySaveManager : MonoBehaviour
{
  
    //다른 스크립트에서 은행 상품에 대해 정해야 함
    //이 클래스에서는 금리와 재산 변동에 대해서만 다루면 됨(자유 예적금 , 복리, 변동금리)
    // 1. 상품에 가입한다. 
    // 2. 가입한 일자부터 금액에 대해 해당 년도의 금리를 받는다. 
    // 3. 주가 변경될 떄 이자가 들어오고 
    // 4. 금리가 변동된다. 
    // 5. 상품에서 현금으로 돈을 뺄 수 있다.
    
    // 해당년도의 금리 적용 
    // 돈을 넣을 수 있음
    // 주가 바뀔때 이자와 합쳐서 입금 
    // 돈 뺄 수 있음

    private double currentSaveMoney = 0;
    private double interestPrice = 0;

    private void Awake()
    {
        RoundSystem.Instance.onRoundChange += Interest;
    }

    public void asseddion()
    {
        // 만든 통장 정보 보여주기 및 백엔드에 전달
    }

    public void Deposit(double depositeMoney) // 처음 예치할 때 시행 
    {
        if (ConsumptionManager.Instance.Consumption(depositeMoney)) // 나중에 상태에 따라 분리하기('뭐'를 못 사는 상황인지)
        {
            Debug.Log("입금 가능");
            currentSaveMoney += depositeMoney; 
            PersonalFinancialManager.Instance.OutputMoney(depositeMoney);
        }
        //입금 창에서 넣을 돈 입금 -> inputfield.text 
        //암튼 정보 받아서 넣음
    }

    public void Interest(int year) // 이자
    {
        double interest = GoogleSheetManager.Instance.YearlyDataGet(year).Rate;

        interestPrice = currentSaveMoney * (interest/100); // 이자 게산

        currentSaveMoney += interestPrice;
    }

    public void Withdrawal(double withdrawalMoney)
    {
        // 출금 창에서 뺼 돈 입력 
        if (currentSaveMoney >= withdrawalMoney)
        {
            currentSaveMoney -= withdrawalMoney;
            Debug.Log("출금 가능");
            PersonalFinancialManager.Instance.InputMoney(withdrawalMoney);
        }
        else
        {
            Debug.Log("출금 불가능");
        }
    }
}
